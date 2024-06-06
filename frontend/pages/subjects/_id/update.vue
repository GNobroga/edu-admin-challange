<template>
  <menu class="menu">
  <form @submit.prevent="validateForm" class="bg-white  animate-slide-in px-7 py-10 max-w-[45vw] w-full shadow-lg rounded-md flex flex-col gap-3">
    <div class="flex justify-between">
      <h1 class="font-bold text-2xl mb-2">Novo</h1>
      <button type="button" @click="$router.push('/subjects')" class="fab-button bg-blue-800 relative bottom-2 text-white">
        <i class="bi bi-x text-3xl"></i>
      </button>
    </div>
    <div  class="flex gap-3">
          <div class="flex flex-col gap-2 flex-1">
            <label class="opacity-80">Nome</label>
            <select ref="selectName" v-model="model.name" class="border rounded-md border-black border-opacity-30 text-lg py-4 px-4 outline-none">
              <option value="" selected disabled>Selecione</option>
              <option v-for="(subject, index) of subjects" :key="index" v-bind:value="subject">{{  subject  }}</option>
            </select>
            <span v-if="errors.name" class="text-xs text-red-600 ms-2">{{ errors.name  }}</span>
          </div>
       </div>

    <div class="flex flex-col gap-2 flex-1">
        <label class="opacity-80">Sala</label>
        <SelectOption @id="setClassId" :item="model.class" :options="classOptions"/>
        <span v-if="errors.classId" class="text-xs text-red-600 ms-2">{{ errors.classId  }}</span>
    </div>


   <div class="flex flex-col gap-2 flex-1">
        <label class="opacity-80">Professor</label>
        <SelectOption @id="setTeacherId" :item="model.teacher" :excludeProperties="['type']" :options="teacherOptions"/>
          <span v-if="errors.teacherId" class="text-xs text-red-600 ms-2">{{ errors.teacherId  }}</span>
    </div>

    <span class="flex justify-between mt-5 gap-3">
      <button  class=" bg-blue-800 hover:opacity-90 flex-1 text-white py-4 rounded-md uppercase shadow-lg font-medium">Salvar</button>
    </span>

  </form>
</menu>
</template>

<script lang="ts">
import { ApiConfig } from '~/environments/api-config';
import { Class } from '~/models/class';
import { User } from '~/models/user';
import { apiRequest } from '~/utils/api-request';
const subjects = [
    "Língua Portuguesa",
    "Matemática",
    "História",
    "Geografia",
    "Educação Física",
    "Artes",
    "Inglês",
    "Física",
    "Química",
    "Biologia",
    "Sociologia",
    "Filosofia",
    "Matemática",
    "Física",
    "Química",
    "Geologia",
    "Biologia",
  ];


export default {
  data: () => ({
    subjects,
    model: {
      id: '',
      name: '',
      classId: null as null | number,
      teacherId: null as null | number,
      class: {} as Class,
      teacher: {} as User,
    },
    errors: {
      name: '',
      teacherId: '',
      classId: '',
    },
    classOptions: [] as Class[],
    teacherOptions: [] as User[],
  }),
  async created() {

    const id = this.$router.currentRoute.params.id;

    await apiRequest(ApiConfig.baseUrlWith(`subjects/${id}`), undefined, data => {
      this.model = data as any;
      this.model.classId = this.model.class.id;
      this.model.teacherId = this.model.teacher.id;
    }, ({ errors }) => {
      errors.forEach(window.alert);
      this.$router.push('/subjects');
    });
    apiRequest(ApiConfig.baseUrlWith('users'), undefined, data => this.teacherOptions = (data as User[]).filter(obj => obj.type === 'TEACHER'));
    apiRequest(ApiConfig.baseUrlWith('classes'), undefined, data => this.classOptions = data as any);
  },
  mounted() {
    setTimeout(() => this.$refs.selectName.value = this.model.name, 300);
  },
  methods: {
    setClassId(obj: { id: number | null}) {
      this.model.classId = obj.id as number;
    },
    setTeacherId(obj: { id: number | null }) {
      this.model.teacherId = obj.id;
    },
    async validateForm() {
      this.errors = { } as any;

      if (this.model.classId === null) {
        this.errors.classId = "O campo é obrigatório";
      }

      if (this.model.teacherId === null) {
        this.errors.teacherId = "O campo é obrigatório";
      }

      if (!this.hasAnyError(this.errors)) {
        await apiRequest(ApiConfig.baseUrlWith(`subjects/${this.model.id}`), this.model, () => {
          (this.$parent as any)?.fetchData();
          this.$router.push('/subjects');
        }, ({ errors }) => errors.forEach(window.alert), { method: 'PUT' });
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
  }
}

</script>
