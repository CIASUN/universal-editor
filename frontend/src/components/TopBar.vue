<template>
  <header class="header">
    <div class="date">
      <label for="monthPicker">Дата:</label>
      <input
        id="monthPicker"
        type="month"
        v-model="localMonth"
        @change="onMonthChange"
      />
    </div>

    <div class="info">
      <span class="badge">НПЗ – закрыто: {{ status.npz }}</span>
      <span class="badge">Добыча – закрыто: {{ status.dob }}</span>
      <span class="badge">НАД – закрыто: {{ status.nad }}</span>
    </div>

    <div class="actions">
      <button class="btn" @click="$emit('refresh')">Обновить</button>
      <button class="btn primary" @click="$emit('save')">Сохранить</button>
      <button class="btn" @click="$emit('export')">Экспорт в Excel</button>
      <button class="btn" @click="$emit('settings')">Настройки</button>
      <button class="btn" @click="$emit('help')">Помощь</button>
    </div>
  </header>
</template>

<script setup>
import { ref, watch } from 'vue';

const props = defineProps({
  date: { type: String, required: true },
  status: { type: Object, default: () => ({ npz: '-', dob: '-', nad: '-' }) }
});

const emit = defineEmits(['change-date', 'refresh', 'save', 'export', 'settings', 'help']);
const localMonth = ref(props.date);

// если props.date не задан, ставим текущий месяц
if (!props.date) {
  const now = new Date();
  const year = now.getFullYear();
  const month = String(now.getMonth() + 1).padStart(2, '0');
  localMonth.value = `${year}-${month}`;
  emit('change-date', localMonth.value);
}

watch(() => props.date, (d) => {
  if (d !== localMonth.value) localMonth.value = d;
});

function onMonthChange() {
  emit('change-date', localMonth.value);
}
</script>

<style scoped>
.header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 8px 12px;
  background: #f5f5f5;
  border-bottom: 1px solid #ddd;
}

.date {
  display: flex;
  align-items: center;
  gap: 6px;
}

.info {
  display: flex;
  gap: 8px;
}

.badge {
  background: #eaeaea;
  padding: 4px 8px;
  border-radius: 4px;
  font-size: 13px;
}

.actions {
  display: flex;
  gap: 6px;
}

.btn {
  padding: 4px 10px;
  background: #eee;
  border: 1px solid #ccc;
  border-radius: 4px;
  cursor: pointer;
}

.btn.primary {
  background: #2e7df6;
  color: white;
  border-color: #2e7df6;
}

.btn:hover {
  background: #ddd;
}
</style>
