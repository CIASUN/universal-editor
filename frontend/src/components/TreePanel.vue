<template>
  <div class="tree-panel" :style="{ width: menuWidth + 'px' }">
    <div class="tree-header">
      <button @click="toggleCollapse">
        {{ isCollapsed ? '▶' : '◀' }}
      </button>
      <span v-if="!isCollapsed">Меню</span>
    </div>

    <div class="tree-content" v-if="!isCollapsed">
      <ul>
        <li v-for="item in menu" :key="item.id">
          {{ item.title }}
          <ul v-if="item.children && item.children.length">
            <li v-for="child in item.children" :key="child.id">
              {{ child.title }}
            </li>
          </ul>
        </li>
      </ul>
    </div>

    <div class="resizer" @mousedown="startResize"></div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';

const menuWidth = ref(200); // начальная ширина
const isCollapsed = ref(false);
const menu = ref([]);

const toggleCollapse = () => {
  isCollapsed.value = !isCollapsed.value;
  if (isCollapsed.value) {
    menuWidth.value = 30; // минимальная ширина при схлопывании
  } else {
    menuWidth.value = 200; // восстанавливаем ширину
  }
};

const startResize = (e) => {
  if (isCollapsed.value) return;

  const startX = e.clientX;
  const startWidth = menuWidth.value;

  const onMouseMove = (e) => {
    const newWidth = startWidth + (e.clientX - startX);
    menuWidth.value = Math.max(100, newWidth); // минимальная ширина 100px
  };

  const onMouseUp = () => {
    window.removeEventListener('mousemove', onMouseMove);
    window.removeEventListener('mouseup', onMouseUp);
  };

  window.addEventListener('mousemove', onMouseMove);
  window.addEventListener('mouseup', onMouseUp);
};

onMounted(async () => {
  try {
    const apiUrl = import.meta.env.VITE_API_URL;
    const res = await fetch(`${apiUrl}/api/menu`);
    menu.value = await res.json();
  } catch (err) {
    console.error('Ошибка загрузки меню:', err);
  }
});
</script>

<style scoped>
.tree-panel {
  position: relative;
  background: #f5f5f5;
  border-right: 1px solid #ccc;
  height: 100vh;
  display: flex;
  flex-direction: column;
  transition: width 0.2s;
}

.tree-header {
  display: flex;
  align-items: center;
  padding: 5px;
  background: #ddd;
  font-weight: bold;
}

.tree-header button {
  margin-right: 5px;
}

.tree-content {
  flex: 1;
  overflow: auto;
  padding: 5px;
}

.resizer {
  width: 5px;
  cursor: ew-resize;
  background: transparent;
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
}
</style>
