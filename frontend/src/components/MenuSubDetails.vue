<template>
  <div class="subdetails">
    <div class="header">
      <strong>{{ formattedMonthYear }}</strong>
    </div>

    <table v-if="rows.length">
      <thead>
        <tr>
          <th>ДО</th>
          <th style="text-align:right">Значение</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="r in rows" :key="r.doName">
          <td>{{ r.doName }}</td>
          <td style="text-align:right">
            {{ r.value.toLocaleString('ru-RU', { minimumFractionDigits: 2 }) }}
          </td>
        </tr>
      </tbody>
    </table>

    <p v-if="!rows.length && !loading">Выберите пункт слева</p>
  </div>
</template>

<script setup>
import { ref, watch, computed } from 'vue';

const props = defineProps({
  menuKey: { type: String, required: true },
  selectedId: { type: Number, default: null },
  selectedMonth: { type: String, default: '' } // формат YYYY-MM
});

const rows = ref([]);
const loading = ref(false);

const formattedMonthYear = computed(() => {
  if (!props.selectedMonth) return '';
  const [year, month] = props.selectedMonth.split('-');
  const dateObj = new Date(year, month - 1);
  const monthName = dateObj.toLocaleString('ru-RU', { month: 'long' });
  return `${monthName.charAt(0).toUpperCase() + monthName.slice(1)} ${year}`;
});

async function loadDetails() {
  if (!props.menuKey || !props.selectedId || !props.selectedMonth) return;
  loading.value = true;
  try {
    const res = await fetch(
      `http://localhost:5000/api/menu/itemdetails/${props.menuKey}/${props.selectedId}?date=${props.selectedMonth}-01`
      // 👆 добавляем параметр date (преобразуем YYYY-MM → YYYY-MM-01)
    );
    if (!res.ok) throw new Error('Ошибка загрузки деталей');
    const data = await res.json();
    rows.value = data.details;
  } catch (e) {
    console.error(e);
  } finally {
    loading.value = false;
  }
}

// Следим за изменением пункта меню или месяца
watch(() => [props.selectedId, props.selectedMonth], loadDetails);
</script>

<style scoped>
.subdetails {
  flex: 1;
  padding: 8px 16px;
  overflow-y: auto;
}
.header {
  margin-bottom: 12px;
  font-size: 16px;
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
