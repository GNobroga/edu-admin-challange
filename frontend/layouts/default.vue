<template>
  <div>
    <header class="bg-gradient-to-r relative from-blue-500 to-teal-400 flex justify-between p-3 items-center shadow-md">
        <a href="index.html" class="text-xl text-white font-bold ps-3 flex gap-3 cursor-pointer">
            <span class="flex gap-2 items-center">
              <i class="bi bi-book-fill"></i>
              EduAdmin
            </span>
            <img src="assets/icon/cliente.png" alt class="w-6">
        </a>
        <Navigation/>
    </header>

        <div>
          <Nuxt/>
        </div>

        <footer class="bg-gradient-to-l flex justify-center text-white from-blue-500 to-teal-400 py-5 absolute bottom-0 left-0 w-full">
          <p class=text-medium>Feito com amor por Gabriel Cardoso ✌️</p>
        </footer>
  </div>
</template>

<script>
  export default {

  }
</script>
