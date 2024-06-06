<template>
  <div>


    <!-- <nuxt-link to="/sobre">Ir para a página Sobre</nuxt-link> -->
    <div class="translateX w-full mx-auto mt-8 max-w-[70vw] shadow-xl bg-white border border-black border-opacity-70">
        <span class="bg-gradient-to-r from-blue-500 to-teal-400 flex justify-between p-3">
            <h2 class="p-3 rounded-md bg-brand-secondary text-white font-semibold text-xl">Gerenciar Alunos</h2>
            <section class="flex gap-3 items-center">
                <button @click="$router.push('/students/new')" id="modalBtnClient" class="fab-button bg-blue-800 text-white">
                  <i class="bi bi-person-plus-fill text-2xl"></i>
                </button>
            </section>
        </span>
        <SearchInput @search="search"/>
       <div class="max-h-[40vh] overflow-hidden overflow-y-auto">
        <table class="w-full bg-white">
            <thead class="text-base">
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
                <td><span class="p-1 bg-yellow-500 rounded-md text-white">{{  student.email }}</span></td>
                <td>
                  <div class="flex gap-3 items-center py-3">
                    <button @click="$router.push(`/students/${student.id}/update`)" class="fab-mini-button bg-orange-500 text-white"><i class="bi bi-pencil-fill text-lg"></i></button>
                    <button @click="showConfirmDelation = true" class="fab-mini-button bg-red-500 text-white"> <i class="bi bi-trash3-fill text-lg"></i></button>
                    <button  @click="$router.push(`/students/${student.id}/averages`)" class="fab-mini-button bg-blue-800">
                      <i class="bi bi-clipboard-data-fill text-lg text-white"></i>
                    </button>
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
      <Paginator @onSelection="paginatorChange" :totalElements="data.length"/>
    </div>
    <NuxtChild/>

  </div>
</template>

<script lang="ts">

import { PageModel } from '~/components/Paginator.vue';
import { ApiConfig } from '~/environments/api-config';
import { User } from '~/models/user';
import { apiRequest } from '~/utils/api-request';


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
        await apiRequest(ApiConfig.baseUrlWith('users/'+id), null, undefined, undefined, {
          method: 'delete',
        });
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
