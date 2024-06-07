<template>
  <div>
    <div class="translateX w-full mx-auto mt-8 max-w-[80vw] shadow-xl bg-white border border-black border-opacity-70">
        <span class="bg-gradient-to-r from-blue-500 to-teal-400 flex justify-between p-3">
            <h2 class="p-3 rounded-md bg-brand-secondary text-white font-semibold text-xl">Gerenciar Notas</h2>
            <section class="flex gap-3 items-center">
                <button @click="$router.push('/grades/new')" id="modalBtnClient" class="fab-button bg-blue-800 text-white">
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
                  <span class="py-2 px-3 block">ID</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Nota</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Professor</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Email do Professor</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Aluno</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Email do Aluno</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">ID da Matéria</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Matéria</span>
              </th>
                    <th class="font-semibold xl:text-start text-[#5E6B8C]">
                      <span>Ações</span>
                    </th>
                </tr>
            </thead>
            <tbody class="bg-white">
              <tr v-for="grade in data" :key="grade.id" class="text-base text-center xl:text-start border border-black border-opacity-20">
                <td class="p-3"> {{  grade.id }}</td>
                <td>{{ grade.value }}</td>
                <td>{{ grade.subject.teacher.name }}</td>
                <td>{{ grade.subject.teacher.email }}</td>
                <td>{{ grade.student.name }}</td>
                <td>{{ grade.student.email }}</td>
                <td>{{ grade.subject.id }}</td>
                <td>{{ grade.subject.name }}</td>
                <td>
                  <div class="flex gap-3 items-center py-3">
                    <button @click="$router.push(`/grades/${grade.id}/update`)" class="fab-mini-button bg-orange-500 text-white"><i class="bi bi-pencil-fill"></i></button>
                    <button @click="deleteById(grade.id)" class="fab-mini-button bg-red-500 text-white"> <i class="bi bi-trash3-fill"></i></button>
                  </div>
                </td>
              </tr>
              <tr v-if="!data.length">
                <td colspan="9">
                  <p class="text-center py-4">Não há nenhum nota</p>
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
  import { Grade } from '~/models/grade';
  import { User } from '~/models/user';
  import { apiRequest } from '~/utils/api-request';


  export default {
    layout: 'default',
    data() {
      return {
        data: [] as Grade[],
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
        const req = await fetch(ApiConfig.baseUrlWith(`grades?page=${pageModel?.currentPage ?? 0}&size=${pageModel?.currentSize ?? 10}`));
        this.data = (await req.json()).data;
      },
      async deleteById(id: number) {
        if (!window.confirm('Realmente deseja deletar?')) return;
        await apiRequest(ApiConfig.baseUrlWith('grades/'+id), null, undefined, undefined, {
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

        const req = await fetch(ApiConfig.baseUrlWith(`grades/search?term=${data.value}`));
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

</style>
