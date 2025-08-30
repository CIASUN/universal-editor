<template>
<div class="app-shell">
<TopBar
:date="date"
:status="status"
@change-date="setDate"
@refresh="onRefresh"
@save="onSave"
@export="onExport"
@settings="onSettings"
@help="onHelp"/>


<div class="main">
<TreePanel @select="onSelect"/>
<section class="content">
<h2 style="margin:0 0 8px">{{ currentTitle }}</h2>
<p style="color:var(--muted)">Здесь будет содержимое выбранного раздела.</p>
</section>
</div>
</div>
</template>


<script setup>
import { ref, computed } from 'vue';
import TopBar from './components/TopBar.vue';
import TreePanel from './components/TreePanel.vue';


const date = ref(new Date().toISOString().slice(0,10));
const status = ref({ npz: '07:05:14', dob: '06:12:25', nad: '-' });
const selectedKey = ref('');


const titles = {
'incoming-plan': 'Входная план (из сетевого графика)',
'incoming-fact': 'Входная факт (из ДОУМ)',
'commercial-plan': 'Коммерческий план',
'plan-no-dz': 'План без ДЗ',
'fact-first-day': 'Факт на 1-е число месяца',
'network-comments': 'Сетевой график (комментарии)',
'util-coeff': 'Коэффициенты эксплуатации',
'quotas': 'Квоты и директивы',
'month-expected': 'Ожидаемая на месяц (посуточно)',
'business-plan': 'Бизнес план',
'network': 'Сетевой график'
};


const currentTitle = computed(() => titles[selectedKey.value] || 'Выберите пункт слева');


function setDate(d) { date.value = d; }
function onSelect(key) { selectedKey.value = key; }
function onRefresh() { console.log('refresh'); }
function onSave() { console.log('save'); }
function onExport() { console.log('export to excel'); }
function onSettings() { console.log('settings'); }
function onHelp() { console.log('help'); }
</script>