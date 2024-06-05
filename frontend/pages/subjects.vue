<template>
  <div>
    <div class="w-full mx-auto mt-8 max-w-[70vw] shadow-xl bg-white border border-black border-opacity-70">
        <span class="bg-gradient-to-r from-blue-500 to-teal-400 flex justify-between p-3">
            <h2 class="p-3 rounded-md bg-brand-secondary text-white font-semibold text-xl">Gerenciar Disciplinas</h2>
            <section class="flex gap-3 items-center">
                <button @click="$router.push('/subjects/new')" id="modalBtnClient" class="fab-button bg-blue-800 text-white">
                  <i class="bi bi-file-earmark-plus-fill text-2xl"></i>
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
                        <span class="py-2 block">Disciplina</span>
                    </th>
                    <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                        <span class="py-2 block">Classe</span>
                    </th>
                    <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                        <span class="py-2 block">Professor</span>
                    </th>
                    <th class="font-semibold xl:text-start text-[#5E6B8C]">
                        <span class="py-2 block">Email do Professor</span>
                    </th>
                    <th class="font-semibold xl:text-start text-[#5E6B8C]">
                      <span>Ações</span>
                    </th>
                </tr>
            </thead>
            <tbody class="bg-white">
              <tr v-for="subject in data" :key="subject.id" class="text-base text-center xl:text-start border border-black border-opacity-20">
                <td class="p-3"> {{  subject.id }}</td>
                <td>{{  subject.name }}</td>
                <td>{{  subject.class.name }}</td>
                <td>{{  subject.teacher.name }}</td>
                <td>{{  subject.teacher.email }}</td>
                <td>
                  <div class="flex gap-3 items-center py-3">
                    <button @click="$router.push(`/subjcts/${subjct.id}/update`)" class="w-8 h-8 rounded-full hover:opacity-80 bg-orange-500 text-white"><i class="bi bi-pencil-fill"></i></button>
                    <button @click="showConfirmDelation = true" class="w-8 h-8 rounded-full hover:opacity-80 bg-red-500 text-white"> <i class="bi bi-trash3-fill"></i></button>
                  </div>
                </td>
                <ConfirmDeletion v-if="showConfirmDelation" @close="showConfirmDelation = false" @confirm="deleteById(subjct.id)"/>
              </tr>
              <tr v-if="!data.length">
                <td colspan="6">
                  <p class="text-center py-4">Não há nenhum disciplinas</p>
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
import { Subject } from '~/models/subject';
  import { User } from '~/models/user';
  import { apiRequest } from '~/utils/api-request';


  export default {
    layout: 'default',
    data() {
      return {
        data: [] as Subject[],
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
        const req = await fetch(ApiConfig.baseUrlWith(`subjects?page=${pageModel?.currentPage ?? 0}&size=${pageModel?.currentSize ?? 10}`));
        this.data = (await req.json()).data;
      },
      async deleteById(id: number) {
        await apiRequest(ApiConfig.baseUrlWith('subjects/'+id), null, undefined, undefined, {
          method: 'delete',
        });
        await this.fetchData();
      },
      async search(data: { value: string }) {
        if (data.value.trim() === '') {
          await this.fetchData();
          return;
        }

        const req = await fetch(ApiConfig.baseUrlWith(`subjects/search?term=${data.value}`));
        this.data = (await req.json()).data;
      }
    },

  };

</script>

<style scoped>

</style>
