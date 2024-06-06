<template>
  <menu class="menu !bg-opacity-20">
    <div class="bg-white max-w-[30vw] w-full p-5 animate-slide-in rounded-md">
      <h1 class="text-lg font-medium">Aviso</h1>
      <p class="mt-3">Tem certeza que deseja prosseguir?</p>
      <div class="flex justify-end gap-2 mt-3">
        <button @click="$emit('close')" class="w-[25%] hover:bg-gray-200 rounded-md shadow-lg">Sair</button>
        <button @click="$emit('close'); $emit('confirm')"class="w-[25%] bg-blue-500 hover:opacity-80 text-white rounded-md shadow-lg py-3">Confirmar</button>
      </div>
    </div>
  </menu>
</template>

<script lang="ts">
  export default {}
</script>
