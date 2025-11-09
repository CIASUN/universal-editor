<template>
  <div class="sublist">
    <h3>Раздел</h3>
    <ul>
      <li
        v-for="item in items"
        :key="item.id"
        :class="{ active: item.id === selectedId }"
        @click="$emit('select', item.id)"
      >
        {{ item.name }}
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, watch, onMounted } from 'vue';

const props = defineProps({
  menuKey: String,
  selectedId: Number
});

const items = ref([]);

async function loadData() {
  if (!props.menuKey) return;
  try {
    const res = await fetch(`http://localhost:5000/api/menu/items/${props.menuKey}`);
    if (!res.ok) throw new Error('Ошибка загрузки списка');
    items.value = await res.json();
  } catch (e) {
    console.error(e);
    items.value = [];
  }
}

onMounted(loadData);

watch(() => props.menuKey, loadData);
</script>

<style scoped>
.sublist {
  width: 200px;
  border-right: 1px solid #ccc;
  padding: 8px;
  overflow-y: auto;
}

.sublist h3 {
  margin: 0 0 8px;
  font-size: 14px;
}

ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

li {
  padding: 4px 6px;
  cursor: pointer;
  border-radius: 4px;
}

li:hover {
  background: #eee;
}

li.active {
  background: #cde4ff;
}
</style>
