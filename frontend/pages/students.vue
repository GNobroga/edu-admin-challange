<template>
  <div>


    <!-- <nuxt-link to="/sobre">Ir para a página Sobre</nuxt-link> -->
    <div class="w-full mx-auto mt-8 max-w-[70vw] shadow-xl bg-white border border-black border-opacity-70">
        <span class="bg-gradient-to-r from-blue-500 to-teal-400 flex justify-between p-3">
            <h2 class="p-3 rounded-md bg-brand-secondary text-white font-semibold text-xl">Gerenciar Alunos</h2>
            <section class="flex gap-3 items-center">
                <button @click="$router.push('/students/update')" id="modalBtnClient" class="bg-green-600 hover:bg-green-400 text-white shadow flex gap-2 items-center hover:opacity-90  px-3 rounded-sm text-sm py-2 font-semibold">
                    Adicionar novo
                </button>
            </section>
        </span>
        <SearchInput @search="search"/>
       <div class="max-h-[50vh] overflow-hidden overflow-y-auto">
        <table class="w-full bg-white">
            <thead class="text-sm">
               <tr class="p-2">
                    <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                        <span class="py-2 px-3 block">ID</span>
                    </th>
                    <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                        <span class="py-2 block">Nome</span>
                    </th>
                    <th class="font-semibold xl:text-start text-[#5E6B8C]">
                        <span class="py-2 block">Email</span>
                    </th>
                    <th class="font-semibold xl:text-start text-[#5E6B8C]">
                      <span>Ações</span>
                    </th>
                </tr>
            </thead>
            <tbody class="bg-white">
              <tr v-for="student in data" :key="student.id" class="text-base text-center xl:text-start border border-black border-opacity-20">
                <td class="p-3"> AL{{  student.id }}</td>
                <td>{{  student.name }}</td>
                <td>{{  student.email }}</td>
                <td>
                  <div class="flex gap-3 items-center py-3">
                    <button @click="$router.push('/students/update?studentId='+student.id)" class="w-8 h-8 rounded-full hover:opacity-80 bg-orange-500 text-white"><i class="bi bi-pencil-fill"></i></button>
                    <button @click="showConfirmDelation = true" class="w-8 h-8 rounded-full hover:opacity-80 bg-red-500 text-white"> <i class="bi bi-trash3-fill"></i></button>
                  </div>
                </td>
                <ConfirmDeletion v-if="showConfirmDelation" @close="showConfirmDelation = false" @confirm="deleteById(student.id)"/>
              </tr>
              <tr v-if="!data.length">
                <td colspan="4">
                  <p class="text-center py-4">Não há nenhum aluno</p>
                </td>
              </tr>
            </tbody>
        </table>
       </div>
      <Paginator @onSelection="paginatorChange"/>
    </div>
    <NuxtChild/>

  </div>
</template>

<script lang="ts">

import { PageModel } from '~/components/Paginator.vue';
import { ApiConfig } from '~/environments/api-config';
import { User } from '~/models/user';


  export default {
    layout: 'default',
    data() {
      return {
        data: [] as User[],
        showConfirmDelation: false,
      }
    },
    created() {
      this.fetchData();
    },
    methods: {
      paginatorChange(pageModel: PageModel) {
        this.fetchData(pageModel);
      },
      async fetchData(pageModel?: PageModel) {
        const req = await fetch(ApiConfig.baseUrlWith(`users?page=${pageModel?.currentPage ?? 0}&size=${pageModel?.currentSize ?? 10}`));
        this.data = (await req.json()).data.filter((user: User) => user.type === 'STUDENT');
      },
      async deleteById(id: number) {
        await fetch(ApiConfig.baseUrlWith('users/'+id), { method: 'delete' });
        await this.fetchData();
      },
      async search(data: { value: string }) {
        if (data.value.trim() === '') {
          await this.fetchData();
          return;
        }

        const req = await fetch(ApiConfig.baseUrlWith(`users/search?term=${data.value}`));
        this.data = (await req.json()).data.filter((user: User) => user.type === 'STUDENT');
      }
    },

  };

</script>

<style scoped>

</style>
