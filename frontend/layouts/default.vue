<template>
  <div>
    <header class="bg-gradient-to-r from-blue-500 to-teal-400 flex justify-between p-3 items-center shadow-md">
        <a href="index.html" class="text-xl text-white font-bold ps-3 flex gap-3 cursor-pointer">
            <span>EduAdmin</span>
            <img src="assets/icon/cliente.png" alt class="w-6">
        </a>
    </header>
        <Navigation/>
        <div>
          <Nuxt/>
        </div>
  </div>
</template>

<script>
  export default {

  }
</script>
