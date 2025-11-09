<template>
  <aside
    class="sidebar"
    :style="{
      width: collapsed ? '5px' : sidebarWidth + 'px',
      boxShadow: collapsed ? '1px 0 2px rgba(0,0,0,0.15)' : '2px 0 4px rgba(0,0,0,0.1)',
    }"
  >
    <!-- Кнопка свернуть/развернуть -->
    <div
      class="collapse-handle"
      :class="{ collapsed }"
      @click="toggleCollapsed"
      title="Свернуть / Развернуть меню"
    >
      {{ collapsed ? '›' : '‹' }}
    </div>

    <div v-show="!collapsed" class="tree">
      <h3>Универсальный редактор</h3>
      <ul>
        <li
		  v-for="item in items"
		  :key="item.key"
		  @click="$emit('select', item)"
		>
		  <span class="dot"></span>{{ item.title }}
		</li>
      </ul>
    </div>

    <!-- Разделительная зона для изменения ширины -->
    <div
      v-show="!collapsed"
      class="resizer"
      @mousedown="startResizeHandle"
    ></div>
  </aside>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount } from "vue";

const collapsed = ref(false);
const sidebarWidth = ref(250);
const minWidth = 80;
const maxWidth = 400;
const items = ref([]);
const loading = ref(true);
const error = ref("");

function toggleCollapsed() {
  collapsed.value = !collapsed.value;
}

// --- Resize logic ---
let isResizing = false;
function startResizeHandle() {
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
    const response = await fetch("http://localhost:5000/api/menu");
    if (!response.ok) throw new Error(`HTTP ${response.status}`);
    items.value = await response.json();
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
  position: relative;
  background: #f6f8fa;
  border-right: 1px solid #ddd;
  overflow: hidden;
  transition: width 0.25s ease;
}

.tree {
  padding: 12px;
  height: 100%;
  overflow-y: auto;
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
  transition: background 0.15s;
}

.tree li:hover {
  background: #e8ecf3;
}

.dot {
  display: inline-block;
  width: 6px;
  height: 6px;
  background: #444;
  border-radius: 50%;
  margin-right: 6px;
}

/* Кнопка свернуть/развернуть */
.collapse-handle {
  position: absolute;
  top: 50%;
  right: -10px;
  transform: translateY(-50%);
  width: 20px;
  height: 40px;
  background: #fff;
  border: 1px solid #ccc;
  border-left: none;
  border-radius: 0 4px 4px 0;
  cursor: pointer;
  font-weight: bold;
  color: #555;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 1px 0 2px rgba(0, 0, 0, 0.2);
  z-index: 10;
  transition: background 0.2s;
}

.collapse-handle:hover {
  background: #f0f0f0;
}

.resizer {
  position: absolute;
  top: 0;
  right: 0;
  width: 4px;
  height: 100%;
  cursor: ew-resize;
  background: transparent;
}
</style>
