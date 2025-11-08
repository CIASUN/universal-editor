<template>
  <aside
    class="sidebar"
    :style="{ width: sidebarWidth + 'px' }"
    @mousemove="onResize"
    @mouseup="stopResize"
  >
    <div class="tree" v-if="!collapsed">
      <h3>Универсальный редактор</h3>
      <ul>
        <li
          v-for="item in items"
          :key="item.key"
          @click="$emit('select', item.key)"
        >
          <span class="dot"></span>{{ item.title }}
        </li>
      </ul>
    </div>

    <!-- Кнопка схлопывания -->
    <div class="collapse-handle" @click="toggleCollapsed">
      <span v-if="collapsed">›</span>
      <span v-else>‹</span>
    </div>

    <!-- Разделитель для изменения ширины -->
    <div
      v-if="!collapsed"
      class="resizer"
      @mousedown="startResizeHandle"
    ></div>
  </aside>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount } from "vue";

const props = defineProps({
  items: { type: Array, default: () => [] }
});

const collapsed = ref(false);
const sidebarWidth = ref(250);
const minWidth = 60;
const maxWidth = 400;

function toggleCollapsed() {
  collapsed.value = !collapsed.value;
  sidebarWidth.value = collapsed.value ? minWidth : 250;
}

// --- Resize logic ---
let isResizing = false;
function startResizeHandle(e) {
  if (collapsed.value) return;
  isResizing = true;
  document.body.style.cursor = "col-resize";
  document.addEventListener("mousemove", onResize);
  document.addEventListener("mouseup", stopResize);
}

function onResize(e) {
  if (!isResizing || collapsed.value) return;
  const newWidth = e.clientX;
  if (newWidth > minWidth && newWidth < maxWidth) {
    sidebarWidth.value = newWidth;
  }
}

function stopResize() {
  if (isResizing) {
    isResizing = false;
    document.body.style.cursor = "default";
    document.removeEventListener("mousemove", onResize);
    document.removeEventListener("mouseup", stopResize);
  }
}

onBeforeUnmount(stopResize);
</script>

<style scoped>
.sidebar {
  display: flex;
  flex-direction: column;
  position: relative;
  background: #f6f8fa;
  border-right: 1px solid #ddd;
  height: 100%;
  transition: width 0.25s ease;
  overflow: hidden;
}

/* Кнопка схлопывания */
.collapse-handle {
  position: absolute;
  top: 50%;
  right: -8px;
  transform: translateY(-50%);
  background: #ddd;
  width: 16px;
  height: 40px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  border-radius: 4px;
  user-select: none;
  font-size: 16px;
  color: #333;
  z-index: 10;
}
.collapse-handle:hover {
  background: #ccc;
}

/* Ручка изменения ширины */
.resizer {
  position: absolute;
  top: 0;
  right: 0;
  width: 4px;
  height: 100%;
  cursor: col-resize;
  background-color: transparent;
}
.resizer:hover {
  background-color: #bbb;
}

/* Стили списка */
.tree {
  padding: 12px;
}
.tree ul {
  list-style: none;
  padding: 0;
  margin: 0;
}
.tree li {
  cursor: pointer;
  padding: 6px 8px;
  border-radius: 4px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.tree li:hover {
  background: #e5e9f0;
}
.dot {
  display: inline-block;
  width: 6px;
  height: 6px;
  background: #444;
  border-radius: 50%;
  margin-right: 6px;
}
</style>
