<template>
  <div class="overflow-hidden bg-gradient-to-r from-blue-500 to-teal-400 ">
    <header class="py-5">
        <span class="text-2xl text-white font-bold ps-3 flex gap-1 cursor-pointer">
            <span class="flex items-center gap-3">EduAdmin <span class="text-base relative top-0.5 opacity-80">- Sistema Escolar</span></span>
        </span>
    </header>
    <main class="b w-screen h-screen flex flex-col items-center pb-5 justify-center gap-3">
        <img src="assets/icon/management.png" alt class="w-20">
        <h1 class="text-6xl font-semibold text-white">Bem-vindo</h1>
        <p class="text-white text-base text-center opacity-90">
            Vamos reunir todos os seus cruds aqui <br/> para simplificar as coisas.
        </p>
        <button v-if="!loading" @click="redirect()" class="bg-white mt-3 text-[#0177D7] w-full py-1.5 text-lg hover:scale-105 transition-all !h-12 max-w-[200px]">Entrar</button>
        <div v-if="loading" class="loader"></div>
    </main>

  </div>
</template>

<script lang="ts">
  export default {
    layout: 'no-layout',
    data: () => ({
      loading: false
    }),
    methods: {
      redirect() {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.$router.push('/students');
        }, 1000);
      }
    }
  }
</script>

<style>
  body {
    overflow: hidden;
  }
</style>
