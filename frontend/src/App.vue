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
      @help="onHelp"
    />

    <div class="main">
      <TreePanel @select="onSelect" />
      <section class="content">
        <h2 style="margin:0 0 8px">{{ currentTitle }}</h2>
        <p style="color:var(--muted)">
          Здесь будет содержимое выбранного раздела.
        </p>
      </section>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue';
import TopBar from './components/TopBar.vue';
import TreePanel from './components/TreePanel.vue';

const date = ref(new Date().toISOString().slice(0, 10));
const status = ref({ npz: '07:05:14', dob: '06:12:25', nad: '-' });
const selectedKey = ref('');
const selectedTitle = ref('');

// Получаем из TreePanel выбранный пункт меню
function onSelect(itemKey, itemTitle) {
  selectedKey.value = itemKey;
  selectedTitle.value = itemTitle;
}

// Заголовок текущего раздела
const currentTitle = computed(() =>
  selectedTitle.value || 'Выберите пункт слева'
);

// Остальные события шапки
function setDate(d) { date.value = d; }
function onRefresh() { console.log('refresh'); }
function onSave() { console.log('save'); }
function onExport() { console.log('export to excel'); }
function onSettings() { console.log('settings'); }
function onHelp() { console.log('help'); }
</script>

<style scoped>
.app-shell {
  display: flex;
  flex-direction: column;
  height: 100vh;
}

.main {
  display: flex;
  flex: 1;
  overflow: hidden;
}

.content {
  flex: 1;
  padding: 16px;
  overflow-y: auto;
}
</style>
