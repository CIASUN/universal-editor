<template>
<header class="header">
<div class="date">
<label for="dt">Дата:</label>
<input id="dt" type="date" v-model="localDate" @change="$emit('change-date', localDate)" />
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
const emit = defineEmits(['change-date','refresh','save','export','settings','help']);
const localDate = ref(props.date);
watch(() => props.date, d => localDate.value = d);
</script>