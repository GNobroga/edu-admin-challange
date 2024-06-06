<template>
  <menu class="menu !items-start">
    <form
      @submit.prevent="validateForm"
      class="bg-white px-7 py-10 max-w-[45vw] w-full shadow-lg rounded-md flex flex-col gap-3 animate-slide-in"
    >
      <div class="flex justify-between">
        <h1 class="font-bold text-2xl mb-2">Novo</h1>
        <button
          type="button"
          @click="$router.push('/grades')"
          class="fab-button bg-blue-800 relative bottom-2 text-white"
        >
          <i class="bi bi-x text-3xl"></i>
        </button>
      </div>

      <div class="flex flex-col gap-2 flex-1">
        <label class="opacity-80">Disciplina</label>
        <SelectOption @id="setSubjectId" :options="subjectsOptions" :excludeProperties="['teacher', 'class']" />
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
        />
        <span v-if="errors.studentId" class="text-xs text-red-600 ms-2">{{
          errors.studentId
        }}</span>
      </div>

      <div class="flex flex-col gap-2 flex-1">
        <label class="opacity-80">Nota</label>
        <input v-model="model.value" type="number" placeholder="Nota" class="border rounded-md border-black border-opacity-30 text-lg py-4 px-4 outline-none">
        <span v-if="errors.value" class="text-xs text-red-600 ms-2">{{
          errors.value
        }}</span>
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
import { Class } from "~/models/class";
import { Subject } from "~/models/subject";
import { User } from "~/models/user";
import { apiRequest } from "~/utils/api-request";

export default {
  data: () => ({
    model: {
      value: 0,
      subjectId: null as null | number,
      studentId: null as null | number,
    },
    errors: {
      name: '',
      studentId: '',
      subjectId: '',
      value: '',
    },
    subjectsOptions: [] as Class[],
    studentsOptions: [] as User[],
  }),
  async created() {
    await apiRequest(
      ApiConfig.baseUrlWith("users"),
      undefined,
      (data) => (this.studentsOptions = (data as User[]).filter(obj => obj.type === 'STUDENT'))
    );
    apiRequest(
      ApiConfig.baseUrlWith("subjects"),
      undefined,
      (data) => (this.subjectsOptions = data as Subject[])
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


      if (parseInt(this.model.value) < 0 || parseInt(this.model.value) > 100) {
        this.errors.value = "O campo deve estar entre 0 e 100";
      }

      if (!this.hasAnyError(this.errors)) {
        await apiRequest(
          ApiConfig.baseUrlWith("grades"),
          this.model,
          () => {
            (this.$parent as any)?.fetchData();
            this.$router.push("/grades");
          },
          ({ errors }) => errors.forEach(window.alert),
          { method: "POST" }
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
