<template>
  <div class="value-editor">
    <aside class="subframe">
      <h4>Список ({{ key }})</h4>
      <ul v-if="items.length">
        <li v-for="item in items" :key="item.id" @click="selectItem(item)">
          {{ item.name }}
        </li>
      </ul>
      <p v-else-if="loading">Загрузка...</p>
      <p v-else>Нет данных</p>
    </aside>

    <section class="details">
      <h4 v-if="selectedItem">Выбран: {{ selectedItem.name }}</h4>
      <p v-else>Выберите элемент слева.</p>
    </section>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue';

const props = defineProps({
  keyValue: { type: String, required: true }
});

const key = ref(props.keyValue);
const items = ref([]);
const loading = ref(true);
const selectedItem = ref(null);

async function loadData() {
  loading.value = true;
  items.value = [];
  try {
    const response = await fetch(`http://localhost:5000/api/menu/items/${key.value}`);
    if (!response.ok) throw new Error(`Ошибка загрузки списка`);
    items.value = await response.json();
  } catch (err) {
    console.error(err);
  } finally {
    loading.value = false;
  }
}

function selectItem(item) {
  selectedItem.value = item;
}

onMounted(loadData);
watch(() => props.keyValue, (newKey) => {
  key.value = newKey;
  loadData();
});
</script>

<style scoped>
.value-editor {
  display: flex;
  height: 100%;
}

.subframe {
  width: 200px;
  border-right: 1px solid #ccc;
  padding: 8px;
  background: #fafafa;
}

.subframe ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

.subframe li {
  padding: 4px 6px;
  cursor: pointer;
}

.subframe li:hover {
  background: #e5e9f0;
}

.details {
  flex: 1;
  padding: 16px;
}
</style>
