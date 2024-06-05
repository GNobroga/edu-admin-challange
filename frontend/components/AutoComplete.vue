<template>
  <div class="w-full h-full relative">
    <input type="text" placeholder="Pesquisar" class="border w-full rounded-md border-black border-opacity-30 text-lg py-4 px-4 outline-none" v-model="searchInput" @input="updateAutocompleteList"  @keydown.enter="selectItem(null)" ref="autocompleteInput">
    <span v-if="empty" class="text-sm text-red-600">Não há nenhum dado</span>
    <div v-if="showAutocomplete" class="autocomplete-items">
      <div v-for="(item, index) in filteredOptions" :key="index" :class="{ 'autocomplete-item': true }" @click="selectItem(item)">
        {{ item }}
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { ApiConfig } from '~/environments/api-config';
import { apiRequest } from '~/utils/api-request';

  export default {
    props: ['name', 'excludeProperties', 'criteria'],
    data() {
      return {
        searchInput: '',
        showAutocomplete: false,
        filteredOptions: [] as any[],
        empty: false,
        savedId: null as null | number,
      };
    },
    watch: {
      async searchInput(newValue, oldValue) {
        if (newValue.trim() === '') {
          this.$emit('getId', { id: null });
          this.savedId = null;
        }

        await apiRequest(ApiConfig.baseUrlWith(`${this.name}/search?term=${newValue}`), null, (data: any) => {

          this.filteredOptions = data.filter(this.criteria ? this.criteria : () => true).map((obj: any) => {
            if (!this.excludeProperties || !this.excludeProperties.length) {
              return obj;
            }
            const newObj = {} as any;

            for (let key in obj) {
              if (!this.excludeProperties.includes(key)) {
                newObj[key] = obj[key];
              }
            }
            return newObj;
          });
        });

        this.empty = this.savedId === null;
      },
    },
    methods: {
      updateAutocompleteList() {
        this.showAutocomplete = true;
      },
      selectItem(item: any) {
        this.savedId = this.filteredOptions[0]?.id ?? item.id;
        if (!item && this.filteredOptions.length > 0) {
          this.searchInput = this.convertToString(this.filteredOptions[0]);
          this.$emit('getId', { id: this.savedId });
          return;
        }
        this.searchInput = this.convertToString(item);
        this.$emit('getId', { id: this.savedId });
        this.showAutocomplete = false;
      },
      convertToString(obj: any) {
        const values = [] as any;
        for (const key in obj) {
          if (typeof obj[key] != 'object')
            values.push(obj[key]);
        }
        return values.join(' - ');
      }
    }
  };
</script>

<style scoped>
.autocomplete-items {
  position: absolute;
  border-radius: 5px;
  max-height: 150px;
  overflow-y: auto;
  background-color: #f1f1f1;
  z-index: 999;
  @apply w-full;
}

.autocomplete-item {
  padding: 10px;
  cursor: pointer;
}

.autocomplete-item:hover, .highlighted {
  background-color: #e9e9e9;
}
</style>
