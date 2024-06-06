<template>
  <div>
    <button @click="close = !close" class="fab-button bg-[#880364] mr-2 text-white transition-all">
      <i v-if="close" class="bi bi-list text-2xl"></i>
      <i v-if="!close" class="bi bi-x-lg text-2xl"></i>
    </button>
    <nav v-if="!close" class="flex flex-col gap-4 absolute right-2 top-20 fadeIn">
           <a @click="$router.push('/students')"  class="flex cursor-pointer items-center bg-[#880364] text-white relative rounded-xl"  >
                <span class="bg-[#B481A3]  flex items-center justify-center rounded-full absolute -left-2 w-8 h-8 transition-all opacity-0" v-bind:class="{ 'opacity-100': currentPath.includes('students')}">
                  <i class="bi bi-arrow-right"></i>
                </span>
                <span class="py-1.5 ms-4 text-xs px-3 uppercase font-semibold">
                 Alunos
                </span>
            </a>
            <a @click="$router.push('/teachers')" class="flex  cursor-pointer items-center bg-[#880364] text-white relative rounded-xl" >
                <span class="bg-[#B481A3]  flex items-center justify-center rounded-full absolute -left-2 w-8 h-8 opacity-0" v-bind:class="{ 'opacity-100': currentPath.includes('teachers')}">
                  <i class="bi bi-arrow-right"></i>
                </span>
                <span class="py-1.5 ms-4 text-xs px-3 uppercase font-semibold">
                  Professores
                </span>
            </a>
            <a @click="$router.push('/subjects')" class="flex cursor-pointer items-center bg-[#880364] text-white relative rounded-xl">
                <span class="bg-[#B481A3]  flex items-center justify-center rounded-full absolute -left-2 w-8 h-8 opacity-0" v-bind:class="{ 'opacity-100': currentPath.includes('subjects')}">
                  <i class="bi bi-arrow-right"></i>
                </span>
                <span class="py-1.5 ms-4 text-xs px-3 uppercase font-semibold">
                    Disciplinas
                </span>
            </a>
            <a  @click="$router.push('/grades')" class="flex cursor-pointer items-center bg-[#880364] text-white relative rounded-xl">
                <span class="bg-[#B481A3]  flex items-center justify-center rounded-full absolute -left-2 w-8 h-8 opacity-0" v-bind:class="{ 'opacity-100': currentPath.includes('grades')}">
                  <i class="bi bi-arrow-right"></i>
                </span>
                <span class="py-1.5 ms-4 text-xs px-3 uppercase font-semibold">
                    Notas
                </span>
            </a>
            <a @click="$router.push('/classes')" class="flex cursor-pointer items-center bg-[#880364] text-white relative rounded-xl">
                <span class="bg-[#B481A3]  flex items-center justify-center rounded-full absolute -left-2 w-8 h-8 opacity-0" v-bind:class="{ 'opacity-100': currentPath.includes('classes')}">
                  <i class="bi bi-arrow-right"></i>
                </span>
                <span class="py-1.5 ms-4 text-xs px-3 uppercase font-semibold">
                    Salas
                </span>
            </a>
            <a @click="$router.push('/attendances')" class="flex cursor-pointer items-center bg-[#880364] text-white relative rounded-xl">
                <span class="bg-[#B481A3]  flex items-center justify-center rounded-full absolute -left-2 w-8 h-8 opacity-0" v-bind:class="{ 'opacity-100': currentPath.includes('attendances')}">
                  <i class="bi bi-arrow-right"></i>
                </span>
                <span class="py-1.5 ms-4 text-xs px-3 uppercase font-semibold">
                    Chamadas
                </span>
            </a>
        </nav>
  </div>
</template>

<script lang="ts">
  export default {
    data() {
      return {
        currentPath: '',
        close: false,
      }
    },
    created() {
      this.currentPath = this.$router.currentRoute.fullPath;
      this.$router.afterEach(to => this.currentPath = to.name ?? '');
    }
  }
</script>
