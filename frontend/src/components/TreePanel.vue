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
defineProps({
  items: { type: Array, required: true }
});
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
