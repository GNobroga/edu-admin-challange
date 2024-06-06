<template>
  <div>
    <div class="fadeIn w-full mx-auto mt-8 max-w-[70vw] shadow-xl bg-white border border-black border-opacity-70">
        <span class="bg-gradient-to-r from-blue-500 to-teal-400 flex justify-between p-3">
            <h2 class="p-3 rounded-md bg-brand-secondary text-white font-semibold text-xl">Gerenciar Disciplinas</h2>
            <section class="flex gap-3 items-center">
                <button @click="$router.push('/subjects/new')" id="modalBtnClient" class="fab-button bg-blue-800 text-white">
                  <i class="bi bi-file-earmark-plus-fill text-2xl"></i>
                </button>
                <button class="fab-button bg-[#384368] text-white" @click="showSearchByTeacher = true">
                  <i class="bi bi-search text-xl"></i>
                </button>
            </section>
        </span>
        <SearchInput @search="search"/>
        <span v-if="teacher" class="ms-2 py-2 block font-medium text-sm">Pesquisando pelo professor com email: <span class="font-thin">{{ teacher?.email }}</span></span>
       <div class="max-h-[50vh] overflow-hidden overflow-y-auto">
        <table class="w-full bg-white">
            <thead class="text-base">
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
                    <button @click="$router.push(`/subjects/${subject.id}/update`)" class="fab-mini-button bg-orange-500 text-white"><i class="bi bi-pencil-fill"></i></button>
                    <button @click="showConfirmDelation = true" class="fab-mini-button bg-red-500 text-white"> <i class="bi bi-trash3-fill"></i></button>
                  </div>
                </td>
                <ConfirmDeletion v-if="showConfirmDelation" @close="showConfirmDelation = false" @confirm="deleteById(subject.id)"/>
              </tr>
              <tr v-if="!data.length">
                <td colspan="6">
                  <p class="text-center py-4">Não há nenhum disciplina</p>
                </td>
              </tr>
            </tbody>
        </table>
       </div>
      <Paginator @onSelection="paginatorChange" :totalElements="data.length"/>
    </div>
    <NuxtChild/>

    <menu class="menu" v-if="showSearchByTeacher">
      <div class="bg-white p-5 rounded-md shadow-lg max-w-[35vw] w-full animate-slide-in">
        <div class="flex items-center justify-between gap-5">
          <span class="font-medium">Professores</span>
          <button class="fab-button bg-blue-800 text-white" @click="showSearchByTeacher = false; teacherId = null">
            <i class="bi bi-x text-2xl"></i>
          </button>
        </div>
        <div class="bg-orange-400 p-3 mt-4 text-white">
          <p class="text-sm rounded-md">Selecione um professor para verificar as disciplinas que ele é responsável </p>
        </div>
        <div class="mt-5">
          <SelectOption @id="setTeacherId" :excludeProperties="['type']" :options="teachers" />
        </div>
        <button v-if="teacherId" class="raised-button bg-blue-700 text-white mt-5" @click="searchByTeacher()">
          Buscar
        </button>
      </div>
    </menu>
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
        teachers: [] as User[],
        showSearchByTeacher: false,
        teacherId: null as number | null,
        teacher: null as null | User,
      }
    },
    created() {
      this.fetchData();
      apiRequest(ApiConfig.baseUrlWith('users'), undefined, data => this.teachers = (data as any).filter(obj => obj.type === 'TEACHER'));
    },
    methods: {
      searchByTeacher() {
        if (!this.teacherId) return;
        this.showSearchByTeacher = false;//teacher
        apiRequest(ApiConfig.baseUrlWith(`subjects/teacher/${this.teacherId}`), undefined, data => {
          this.data = data as any;
        });
        apiRequest(ApiConfig.baseUrlWith(`users/${this.teacherId}`), undefined, data => {
          this.teacher = data as any;
        });
      },
      setTeacherId(data: { id: number }) {
        this.teacherId = data.id;
      },
      paginatorChange(pageModel: PageModel) {
        this.fetchData(pageModel);
      },
      async fetchData(pageModel?: PageModel) {
        this.reset();
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
        this.reset();
        if (data.value.trim() === '') {
          await this.fetchData();
          return;
        }

        const req = await fetch(ApiConfig.baseUrlWith(`subjects/search?term=${data.value}`));
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
