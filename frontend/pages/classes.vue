<template>
  <div>
    <div class="translateX w-full mx-auto mt-8 max-w-[70vw] shadow-xl bg-white border border-black border-opacity-70">
        <span class="bg-gradient-to-r from-blue-500 to-teal-400 flex justify-between p-3">
            <h2 class="p-3 rounded-md bg-brand-secondary text-white font-semibold text-xl">Gerenciar Salas</h2>
            <section class="flex gap-3 items-center">
                <button @click="$router.push('/classes/new')" id="modalBtnClient" class="fab-button bg-blue-800 text-white">
                  <i class="bi bi-file-earmark-plus-fill text-2xl"></i>
                </button>
            </section>
        </span>
        <SearchInput @search="search"/>
       <div class="max-h-[40vh] overflow-hidden overflow-y-auto">
        <table class="w-full bg-white">
            <thead class="text-base">
               <tr class="p-2">
                <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 px-3 block text-center">ID</span>
                </th>
                <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                    <span class="py-2 block text-center ">Nome</span>
                </th>
                <th class="font-semibold xl:text-start text-[#5E6B8C]">
                  <span class="text-end block pe-9">Ações</span>
                </th>
              </tr>
            </thead>
            <tbody class="bg-white">
              <tr v-for="clazz in data" :key="clazz.id" class="text-base text-center xl:text-start border border-black border-opacity-20">
                <td class="p-3 text-center"> {{  clazz.id }}</td>
                <td class="text-center">{{ clazz.name }}</td>
                <td>
                  <div class="flex justify-end gap-3 items-center py-3 pr-5">
                    <button @click="$router.push(`/classes/${clazz.id}/update`)" class="fab-mini-button bg-orange-500 text-white"><i class="bi bi-pencil-fill"></i></button>
                    <button @click="showConfirmDelation = true" class="fab-mini-button bg-red-500 text-white"> <i class="bi bi-trash3-fill"></i></button>
                  </div>
                </td>
                <ConfirmDeletion v-if="showConfirmDelation" @close="showConfirmDelation = false" @confirm="deleteById(clazz.id)"/>
              </tr>
              <tr v-if="!data.length">
                <td colspan="3">
                  <p class="text-center py-4">Não há nenhuma sala</p>
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
  import { Class } from '~/models/class';
  import { User } from '~/models/user';
  import { apiRequest } from '~/utils/api-request';


  export default {
    layout: 'default',
    data() {
      return {
        data: [] as Class[],
        showConfirmDelation: false,
      }
    },
    created() {
      this.fetchData();
    },
    methods: {
      setTeacherId(data: { id: number }) {
        this.teacherId = data.id;
      },
      paginatorChange(pageModel: PageModel) {
        this.fetchData(pageModel);
      },
      async fetchData(pageModel?: PageModel) {
        this.reset();
        const req = await fetch(ApiConfig.baseUrlWith(`classes?page=${pageModel?.currentPage ?? 0}&size=${pageModel?.currentSize ?? 10}`));
        this.data = (await req.json()).data;
      },
      async deleteById(id: number) {
        await apiRequest(ApiConfig.baseUrlWith('classes/'+id), null, undefined, undefined, {
          method: 'delete',
        });
        await this.fetchData();
      },
      async search(data: { value: string }) {
        this.reset();
        if (data.value.trim() === '') {
          await this.fetchData();
          return;
        }

        const req = await fetch(ApiConfig.baseUrlWith(`classes/search?term=${data.value}`));
        this.data = (await req.json()).data;
      },
      reset() {
        this.teacher = null;
        this.teacherId = null;
      }
    },

  };

</script>

<style scoped>
  thead {
    th {
      width: 33%;
    }
  }
</style>
