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
      <TreePanel :items="menuItems" @select="onSelect" />

      <section class="content">
        <!-- Если выбран ctrlProd_UEdt_ValueEditor -->
        <template v-if="selectedItem.className === 'ctrlProd_UEdt_ValueEditor'">
          <div class="twoframe">
            <MenuSubList :menuKey="selectedItem.key" @select="onSubSelect" />
            <MenuSubDetails
              :menuKey="selectedItem.key"
              :selectedId="selectedSubId"
              :selectedMonth="date"
            />
          </div>
        </template>

        <!-- Для остальных пунктов -->
        <template v-else>
          <h2 style="margin:0 0 8px">{{ currentTitle }}</h2>
          <p style="color:var(--muted)">
            Здесь будет содержимое выбранного раздела.<br />
            <small v-if="selectedItem.className">
              Компонент: {{ selectedItem.className }}
            </small>
          </p>
        </template>
      </section>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import TopBar from './components/TopBar.vue';
import TreePanel from './components/TreePanel.vue';
import MenuSubList from './components/MenuSubList.vue';
import MenuSubDetails from './components/MenuSubDetails.vue';

// --- текущее значение месяца в формате YYYY-MM ---
const now = new Date();
const year = now.getFullYear();
const month = String(now.getMonth() + 1).padStart(2, '0');
const date = ref(`${year}-${month}`);

const status = ref({ npz: '07:05:14', dob: '06:12:25', nad: '-' });
const menuItems = ref([]);
const selectedItem = ref({ key: '', title: '', className: '' });
const selectedSubId = ref(null);

onMounted(async () => {
  try {
    const response = await fetch('http://localhost:5000/api/menu');
    if (!response.ok) throw new Error('Ошибка загрузки меню');
    menuItems.value = await response.json();
  } catch (err) {
    console.error('Ошибка при загрузке меню:', err);
  }
});

function setDate(d) {
  date.value = d;
}

function onSelect(item) {
  selectedItem.value = item;
  selectedSubId.value = null; // сброс выбора при переходе в другой раздел
}

function onSubSelect(id) {
  selectedSubId.value = id;
}

function onRefresh() { console.log('refresh'); }
function onSave() { console.log('save'); }
function onExport() { console.log('export'); }
function onSettings() { console.log('settings'); }
function onHelp() { console.log('help'); }

const currentTitle = computed(() =>
  selectedItem.value?.title || 'Выберите пункт слева'
);
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

.twoframe {
  display: flex;
  height: 100%;
}
</style>
