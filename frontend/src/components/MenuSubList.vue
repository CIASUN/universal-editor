<template>
  <div class="sublist">
    <h3>Раздел</h3>
    <ul>
      <li v-for="item in items"
          :key="item.id"
          @click="$emit('select', item.id)">
        {{ item.name }}
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';

const props = defineProps({
  menuKey: { type: String, required: true }
});
const emit = defineEmits(['select']);

const items = ref([]);
const loading = ref(false);
const error = ref('');

watch(() => props.menuKey, async (newKey) => {
  if (!newKey) return;
  loading.value = true;
  error.value = '';
  try {
    const res = await fetch(`http://localhost:5000/api/menu/items/${newKey}`);
    if (!res.ok) throw new Error('Ошибка загрузки списка');
    items.value = await res.json();
  } catch (e) {
    error.value = e.message;
  } finally {
    loading.value = false;
  }
});
</script>

<style scoped>
.sublist {
  width: 220px;
  border-right: 1px solid #ccc;
  padding: 8px;
}

ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

li {
  cursor: pointer;
  padding: 4px 8px;
}

li:hover {
  background: #e8f0ff;
}
</style>
