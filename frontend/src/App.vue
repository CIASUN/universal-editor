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
      <!-- передаём список меню в TreePanel -->
      <TreePanel :items="menuItems" @select="onSelect" />

      <section class="content">
        <h2 style="margin:0 0 8px">{{ currentTitle }}</h2>
        <p style="color:var(--muted)">Здесь будет содержимое выбранного раздела.</p>
      </section>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import TopBar from './components/TopBar.vue';
import TreePanel from './components/TreePanel.vue';

const date = ref(new Date().toISOString().slice(0,10));
const status = ref({ npz: '07:05:14', dob: '06:12:25', nad: '-' });
const selectedKey = ref('');
const menuItems = ref([]); // список меню с бэкенда

onMounted(async () => {
  try {
    const response = await fetch('http://localhost:5000/api/menu');
    if (!response.ok) throw new Error('Ошибка загрузки меню');
    menuItems.value = await response.json();
  } catch (err) {
    console.error('Ошибка при загрузке меню:', err);
  }
});

const currentTitle = computed(() => {
  const found = menuItems.value.find(i => i.key === selectedKey.value);
  return found ? found.title : 'Выберите пункт слева';
});

function setDate(d) { date.value = d; }
function onSelect(key) { selectedKey.value = key; }
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
