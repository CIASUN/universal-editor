<template>
  <aside class="sidebar">
    <div class="tree">
      <h3>Универсальный редактор</h3>
      <ul>
        <li v-for="item in items" :key="item.key" @click="$emit('select', item.key)">
          <span class="dot"></span>{{ item.title }}
        </li>
      </ul>
    </div>
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
  width: 280px;
  background: #f6f8fa;
  border-right: 1px solid #ddd;
  overflow-y: auto;
}

.tree {
  padding: 12px;
}

.tree ul {
  list-style: none;
  padding: 0;
}

.tree li {
  cursor: pointer;
  padding: 6px 8px;
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
