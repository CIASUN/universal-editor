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
const items = [
{ key: 'incoming-plan', title: 'Входная план (из сетевого графика)' },
{ key: 'incoming-fact', title: 'Входная факт (из ДОУМ)' },
{ key: 'commercial-plan', title: 'Коммерческий план' },
{ key: 'plan-no-dz', title: 'План без ДЗ' },
{ key: 'fact-first-day', title: 'Факт на 1-е число месяца' },
{ key: 'network-comments', title: 'Сетевой график (комментарии)' },
{ key: 'util-coeff', title: 'Коэффициенты эксплуатации' },
{ key: 'quotas', title: 'Квоты и директивы' },
{ key: 'month-expected', title: 'Ожидаемая на месяц (посуточно)' },
{ key: 'business-plan', title: 'Бизнес план' },
{ key: 'network', title: 'Сетевой график' }
];
</script>