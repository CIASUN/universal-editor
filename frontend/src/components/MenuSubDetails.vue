<template>
  <div class="subdetails">
    <div class="header">
      <strong>Дата: {{ date }}</strong>
    </div>

    <table v-if="rows.length">
      <thead>
        <tr>
          <th>ДО</th>
          <th>Значение</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="r in rows" :key="r.doName">
          <td>{{ r.doName }}</td>
          <td style="text-align:right">{{ r.value.toLocaleString('ru-RU', { minimumFractionDigits: 2 }) }}</td>
        </tr>
      </tbody>
    </table>

    <p v-if="!rows.length && !loading">Выберите пункт слева</p>
  </div>
</template>

<script setup>
import { ref, watch } from 'vue';

const props = defineProps({
  menuKey: { type: String, required: true },
  selectedId: { type: Number, default: null }
});
const rows = ref([]);
const date = ref('');
const loading = ref(false);

watch(() => props.selectedId, async (newId) => {
  if (!newId) return;
  loading.value = true;
  try {
    const res = await fetch(`http://localhost:5000/api/menu/itemdetails/${props.menuKey}/${newId}`);
    if (!res.ok) throw new Error('Ошибка загрузки деталей');
    const data = await res.json();
    date.value = data.date;
    rows.value = data.details;
  } catch (e) {
    console.error(e);
  } finally {
    loading.value = false;
  }
});
</script>

<style scoped>
.subdetails {
  flex: 1;
  padding: 8px 16px;
  overflow-y: auto;
}
.header {
  margin-bottom: 12px;
}
table {
  width: 100%;
  border-collapse: collapse;
}
th, td {
  border-bottom: 1px solid #ddd;
  padding: 4px 8px;
}
th {
  text-align: left;
  background: #f5f5f5;
}
</style>
