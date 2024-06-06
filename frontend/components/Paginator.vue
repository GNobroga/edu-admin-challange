<template>
   <section class="flex justify-between items-center p-4">
            <span class="text-sm">Mostrando <strong class="text-[#5E6B8C]" id="countCurrentClientPage">{{ totalElements ?? 0 }}</strong> de <strong class="text-[#5E6B8C]">Indeterminado</strong> entradas</span>
            <div class="paginator flex items-center gap-3 text-sm">
                <button @click="previous()">Anterior</button>
                <select @input="handleSelectChange" class="border border-black p-1 border-opacity-40">
                  <option value="5">5</option>
                  <option value="10" selected>10</option>
                  <option value="25">25</option>
                  <option value="50">50</option>
                  <option value="100">100</option>
                </select>
                <button class="bg-[#EEEEEE] p-2 rounded-sm hover:opacity-80" @click="next()">Próximo</button>
            </div>
        </section>
</template>

<script lang="ts">

  export interface PageModel {
      currentPage: number;
      currentSize: number;
  }

  export default {
    props: ['totalElements'],
    data() {
      return {
        currentPage: 0,
        currentSize: 10,
      };
    },
    methods: {
      next() {
        this.currentPage = this.currentPage + 1;
        this.emitChanges();
      },
      previous() {
        this.currentPage = this.currentPage > 1 ? this.currentPage - 1 : 0;
        this.emitChanges();
      },
      handleSelectChange(event: any) {
        this.currentSize = parseInt(event.target.value);
        this.emitChanges();
      },
      emitChanges() {
        this.$emit('onSelection', { currentPage: this.currentPage, currentSize: this.currentSize });
      }
    },
  };
</script>
