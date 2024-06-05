<template>
  <div>
    <input ref="searchInput" @input="search()" type="text" class="w-full py-4 px-3 bg-white border border-black border-opacity-20 outline-none text-lg" placeholder="Pesquisar...">
  </div>
</template>

<script lang=ts>
  export default {
    methods: {
      search() {
        const value = (this.$refs['searchInput'] as any).value;
        this.$emit('search', { value })
      }
    }
  }
</script>
