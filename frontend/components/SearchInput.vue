<template>
  <div>
    <input ref="searchInput" @input="$emit('search', { value: $refs.searchInput?.value ?? '' })" type="text" class="w-full py-4 px-3 bg-gray-100 border border-black border-opacity-20 outline-none text-lg placeholder:text-black" placeholder="Pesquisar...">
  </div>
</template>

<script lang=ts>
  export default {

  }
</script>
