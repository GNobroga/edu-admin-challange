<template>
  <menu class="menu !items-start">
    <form
      @submit.prevent="validateForm"
      class="bg-white px-7 py-10 max-w-[45vw] w-full shadow-lg rounded-md flex flex-col gap-3 animate-slide-in"
    >
      <div class="flex justify-between">
        <h1 class="font-bold text-2xl mb-2">Atualizar</h1>
        <button
          type="button"
          @click="$router.push('/attendances')"
          class="fab-button bg-blue-800 relative bottom-2 text-white"
        >
          <i class="bi bi-x text-3xl"></i>
        </button>
      </div>

      <div class="flex flex-col gap-2 flex-1 max-w-[30%]">
        <label class="opacity-80">Código</label>
        <input v-model="model.id" type="text" disabled class="border bg-gray-100 rounded-md border-black border-opacity-30 text-lg py-4 px-4 outline-none">
      </div>


      <div class="flex flex-col gap-2 flex-1">
        <label class="opacity-80">Disciplina</label>
        <SelectOption @id="setSubjectId" :options="subjectsOptions" :excludeProperties="['teacher', 'class']" :item="model.subject" />
        <span v-if="errors.subjectId" class="text-xs text-red-600 ms-2">{{
          errors.subjectId
        }}</span>
      </div>

      <div class="flex flex-col gap-2 flex-1">
        <label class="opacity-80">Estudante</label>
        <SelectOption
          @id="setStudentId"
          :excludeProperties="['type']"
          :options="studentsOptions"
          :item="model.student"
        />
        <span v-if="errors.studentId" class="text-xs text-red-600 ms-2">{{
          errors.studentId
        }}</span>
      </div>

      <div class="flex flex-col gap-2 flex-1">
        <label class="opacity-80">Data</label>
        <input v-model="model.date" type="date" class="border rounded-md border-black border-opacity-30 text-lg py-4 px-4 outline-none">
        <span v-if="errors.date" class="text-xs text-red-600 ms-2">{{
          errors.date
        }}</span>
      </div>

      <div class="flex gap-2 items-center text-base mt-4">
        <input v-model="model.present" type="checkbox" class="w-5 h-5">
        Marcar como presente
      </div>

      <span class="flex justify-between mt-5 gap-3">
        <button
          class="bg-blue-800 hover:opacity-90 flex-1 text-white py-4 rounded-md uppercase shadow-lg font-medium"
        >
          Salvar
        </button>
      </span>
    </form>
  </menu>
</template>

<script lang="ts">
import { ApiConfig } from "~/environments/api-config";
import { Attendance } from "~/models/attendance";
import { Class } from "~/models/class";
import { Subject } from "~/models/subject";
import { User } from "~/models/user";
import { apiRequest } from "~/utils/api-request";

export default {
  data: () => ({
    model: {
      id: '',
      subjectId: null as null | number,
      studentId: null as null | number,
      student: {} as User,
      subject: {} as Subject,
      date: null as null | string | Date,
      present: true,
    },
    errors: {
      name: '',
      studentId: '',
      subjectId: '',
      date: ''
    },
    subjectsOptions: [] as Class[],
    studentsOptions: [] as User[],
  }),
  async created() {
    await apiRequest(
      ApiConfig.baseUrlWith(`attendances/${this.$router.currentRoute.params.id}`),
      undefined,
      data => {
        this.model = data as Attendance;
        this.model.studentId = this.model.student.id;
        this.model.subjectId = this.model.subject.id;
      });

    await apiRequest(
      ApiConfig.baseUrlWith("users"),
      undefined,
      data => (this.studentsOptions = (data as User[]).filter(obj => obj.type === 'STUDENT'))
    );
    await apiRequest(
      ApiConfig.baseUrlWith("subjects"),
      undefined,
      data => (this.subjectsOptions = data as Subject[])
    );
  },
  methods: {
    setSubjectId(obj: { id: number | null }) {
      this.model.subjectId = obj.id as number;
    },
    setStudentId(obj: { id: number | null }) {
      this.model.studentId = obj.id;
    },
    async validateForm() {
      this.errors = {} as any;

      if (this.model.studentId === null) {
        this.errors.studentId = "O campo é obrigatório";
      }

      if (this.model.subjectId === null) {
        this.errors.subjectId = "O campo é obrigatório";
      }

      if (!this.model.date) {
        this.errors.date = "O campo é obrigatório";
      }

      if (!this.hasAnyError(this.errors)) {
        await apiRequest(
          ApiConfig.baseUrlWith(`attendances/${this.model.id}`),
          this.model,
          () => {
            (this.$parent as any)?.fetchData();
            this.$router.push("/attendances");
          },
          ({ errors }) => errors.forEach(window.alert),
          { method: "PUT" }
        );
      }
    },
    hasAnyError(errors: object) {
      for (const key in errors) {
        if (errors[key]) {
          return true;
        }
        return false;
      }
    }
  },
};
</script>
