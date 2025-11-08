<template>
  <aside
    class="sidebar"
    :style="{ width: sidebarWidth + 'px' }"
  >
    <div class="resize-handle" @mousedown.stop.prevent="startResizeHandle"></div>

    <div v-if="!collapsed" class="tree">
      <h3>Универсальный редактор</h3>

      <div v-if="loading" class="loading">Загрузка...</div>
      <div v-else-if="error" class="error">Ошибка: {{ error }}</div>
      <ul v-else>
        <li
          v-for="item in items"
          :key="item.key"
          @click="$emit('select', item.key)"
        >
          <span class="dot"></span>{{ item.title }}
        </li>
      </ul>
    </div>

    <div v-else class="collapsed-label" @click="toggleCollapsed">
      ▶
    </div>

    <button class="collapse-btn" @click="toggleCollapsed">
      {{ collapsed ? '⯈' : '⯇' }}
    </button>
  </aside>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount } from "vue";

const collapsed = ref(false);
const sidebarWidth = ref(250);
const minWidth = 60;
const maxWidth = 400;
const items = ref([]);
const loading = ref(true);
const error = ref("");

function toggleCollapsed() {
  collapsed.value = !collapsed.value;
  sidebarWidth.value = collapsed.value ? minWidth : 250;
}

// --- Resize logic ---
let isResizing = false;
function startResizeHandle(e) {
  isResizing = true;
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
  isResizing = false;
  document.removeEventListener("mousemove", onResize);
  document.removeEventListener("mouseup", stopResize);
}
onBeforeUnmount(stopResize);

// --- Fetch menu data ---
async function fetchMenu() {
  loading.value = true;
  error.value = "";
  try {
    const apiUrl = import.meta.env.VITE_API_URL;
    const response = await fetch(`${apiUrl}/api/menu`);

    if (!response.ok) throw new Error(`HTTP ${response.status}`);
    const data = await response.json();
    items.value = data;
  } catch (err) {
    console.error(err);
    error.value = err.message;
  } finally {
    loading.value = false;
  }
}

onMounted(fetchMenu);
</script>

<style scoped>
.sidebar {
  display: flex;
  flex-direction: column;
  position: relative;
  border-right: 1px solid #ccc;
  background: #f7f7f7;
  overflow: hidden;
  transition: width 0.2s ease;
}

.tree {
  flex: 1;
  overflow-y: auto;
  padding: 8px;
}

.resize-handle {
  position: absolute;
  top: 0;
  right: 0;
  width: 4px;
  cursor: ew-resize;
  height: 100%;
  background: transparent;
}

.collapse-btn {
  position: absolute;
  bottom: 4px;
  right: 4px;
  border: none;
  background: #ddd;
  border-radius: 4px;
  cursor: pointer;
  width: 24px;
  height: 24px;
}

.loading,
.error {
  padding: 8px;
  color: #666;
}

.collapsed-label {
  display: flex;
  justify-content: center;
  align-items: center;
  flex: 1;
  cursor: pointer;
}
</style>
