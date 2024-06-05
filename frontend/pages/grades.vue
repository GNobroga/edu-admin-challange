<template>
  <div>
    <div class="w-full mx-auto mt-8 max-w-[70vw] shadow-xl bg-white border border-black border-opacity-70">
        <span class="bg-gradient-to-r from-blue-500 to-teal-400 flex justify-between p-3">
            <h2 class="p-3 rounded-md bg-brand-secondary text-white font-semibold text-xl">Gerenciar Notas</h2>
            <section class="flex gap-3 items-center">
                <button @click="$router.push('/grades/new')" id="modalBtnClient" class="fab-button bg-blue-800 text-white">
                  <i class="bi bi-file-earmark-plus-fill text-2xl"></i>
                </button>
                <button class="p-1 px-3 hover:opacity-80 flex gap-2 rounded-md items-center bg-[#384368] text-white" @click="showSearchByStudent = true">
                  <span class="text-sm">Pesquisar por Aluno</span>
                  <i class="bi bi-search text-lg"></i>
                </button>
                <button class="p-1 px-3 hover:opacity-80 flex gap-2 rounded-md items-center bg-[#384368] text-white" @click="showSearchByTeacher = true">
                  <span class="text-sm">Pesquisar por Professor</span>
                  <i class="bi bi-search text-lg"></i>
                </button>
            </section>
        </span>
        <SearchInput @search="search"/>
        <span v-if="teacher" class="ms-2 py-2 block font-medium text-sm">Pesquisando pelo professor com email: <span class="font-thin">{{ teacher?.email }}</span></span>
       <div class="max-h-[50vh] overflow-hidden overflow-y-auto">
        <table class="w-full bg-white">
            <thead class="text-sm">
               <tr class="p-2">
                  <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 px-3 block">ID</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Nota</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Data</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Nome do Aluno</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Email do Aluno</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">ID da Matéria</span>
              </th>
              <th class="font-semibold text-center xl:text-start text-[#5E6B8C]">
                  <span class="py-2 block">Nome da Matéria</span>
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
                <td>{{ grade.date }}</td>
                <td>{{ grade.student.name }}</td>
                <td>{{ grade.student.email }}</td>
                <td>{{ grade.subject.id }}</td>
                <td>{{ grade.subject.name }}</td>
                <td>
                  <div class="flex gap-3 items-center py-3">
                    <button @click="$router.push(`/grades/${grade.id}/update`)" class="w-8 h-8 rounded-full hover:opacity-80 bg-orange-500 text-white"><i class="bi bi-pencil-fill"></i></button>
                    <button @click="showConfirmDelation = true" class="w-8 h-8 rounded-full hover:opacity-80 bg-red-500 text-white"> <i class="bi bi-trash3-fill"></i></button>
                  </div>
                </td>
                <ConfirmDeletion v-if="showConfirmDelation" @close="showConfirmDelation = false" @confirm="deleteById(grade.id)"/>
              </tr>
              <tr v-if="!data.length">
                <td colspan="6">
                  <p class="text-center py-4">Não há nenhum notas</p>
                </td>
              </tr>
            </tbody>
        </table>
       </div>
      <Paginator @onSelection="paginatorChange"/>
    </div>
    <NuxtChild/>

    <menu class="menu" v-if="showSearchByStudent">
      <div class="bg-white p-5 rounded-md shadow-lg max-w-[35vw] w-full animate-slide-in">
        <div class="flex items-center justify-between gap-5">
          <span class="font-medium">Professores</span>
          <button class="fab-button bg-blue-800 text-white" @click="showSearchByStudent = false; teacherId = null">
            <i class="bi bi-x text-2xl"></i>
          </button>
        </div>
        <div class="bg-orange-400 p-3 mt-4 text-white">
          <p class="text-sm rounded-md">Selecione um aluno para verificar as disciplinas que ele é responsável </p>
        </div>
        <div class="mt-5">
          <SelectOption @id="setStudentId" :excludeProperties="['type']" :options="students" />
        </div>
        <button v-if="studentId" class="raised-button bg-blue-700 text-white mt-5" @click="searchByStudent()">
          Buscar
        </button>
      </div>
    </menu>
  </div>
</template>

<script lang="ts">

  import { PageModel } from '~/components/Paginator.vue';
  import { ApiConfig } from '~/environments/api-config';
  import { grade } from '~/models/grade';
  import { User } from '~/models/user';
  import { apiRequest } from '~/utils/api-request';


  export default {
    layout: 'default',
    data() {
      return {
        data: [] as grade[],
        showConfirmDelation: false,
        teachers: [] as User[],
        students: [] as User[],
        showSearchByStudent: false,
        showSearchByTeacher: false,
        studentId: null as number | null,
        student: null as null | User,
        teacherId: null as number | null,
        teacher: null as null | User,
      }
    },
    created() {
      this.fetchData();
      apiRequest(ApiConfig.baseUrlWith('users'), undefined, data => this.students = (data as any).filter(obj => obj.type === 'STUDENT'));
      apiRequest(ApiConfig.baseUrlWith('users'), undefined, data => this.teachers = (data as any).filter(obj => obj.type === 'TEACHER'));
    },
    methods: {
      searchByStudent() {
        if (!this.studentId) return;
        this.showSearchByStudent = false;
        apiRequest(ApiConfig.baseUrlWith(`grades/student/${this.teacherId}`), undefined, data => {
          this.data = data as any;
        });
        apiRequest(ApiConfig.baseUrlWith(`users/${this.teacherId}`), undefined, data => {
          this.teacher = data as any;
        });
      },
      searchByTeacher() {
        if (!this.teacherId) return;
        this.showSearchByTeacher = false;
        apiRequest(ApiConfig.baseUrlWith(`grades/teacher/${this.teacherId}`), undefined, data => {
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
        const req = await fetch(ApiConfig.baseUrlWith(`grades?page=${pageModel?.currentPage ?? 0}&size=${pageModel?.currentSize ?? 10}`));
        this.data = (await req.json()).data;
      },
      async deleteById(id: number) {
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
