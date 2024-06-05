<template>
      <menu class="bg-black bg-opacity-50 absolute z-[999] top-0 left-0 w-full h-full flex items-center justify-center">
      <form @submit.prevent="validateForm" class="bg-white px-7 py-10 max-w-[45vw] w-full shadow-lg rounded-md flex flex-col gap-3">
        <div class="flex justify-between">
          <h1 class="font-bold text-2xl mb-2">Novo</h1>
          <button type="button" @click="$router.push('/subjects')" class="fab-button bg-blue-800 relative bottom-2 text-white">
            <i class="bi bi-x text-3xl"></i>
          </button>
        </div>
       <div  class="flex gap-3">
          <div class="flex flex-col gap-2 flex-1">
            <label class="opacity-80">Nome</label>
            <input type="text" v-model="model.name" name="name" class="border rounded-md border-black border-opacity-30 text-lg py-4 px-4 outline-none" placeholder="Nome">
            <span v-if="errors.name" class="text-xs text-red-600 ms-2">{{ errors.name  }}</span>
          </div>
       </div>

        <div class="flex flex-col gap-2 flex-1">
            <label class="opacity-80">Sala</label>
            <AutoComplete @getId="setClassId" name="classes"/>
        </div>


       <div class="flex flex-col gap-2 flex-1">
            <label class="opacity-80">Professor</label>
            <AutoComplete @getId="setTeacherId" name="users" :excludeProperties="['type']" :criteria="obj => obj.type === 'TEACHER'"/>
        </div>

        <span class="flex justify-between mt-5 gap-3">
          <button  class=" bg-blue-800 hover:opacity-90 flex-1 text-white py-4 rounded-md uppercase shadow-lg font-medium">Salvar</button>
        </span>

      </form>
    </menu>
</template>

<script lang="ts">
import { ApiConfig } from '~/environments/api-config';
import { apiRequest } from '~/utils/api-request';


  export default {
    data: () => ({
      model: {
        name: '',
        classId: null as null | number,
        teacherId: null as null | number,
      },
      errors: {
        name: '',
      }
    }),
    methods: {
      setClassId(obj: { id: number | null}) {
        this.model.classId = obj.id as number;
      },
      setTeacherId(obj: { id: number | null }) {
        this.model.teacherId = obj.id;
      },
      async validateForm() {
        this.errors = { name: '' };

        if (this.model.name.trim() === '') {
          this.errors.name = 'O campo é obrigatório';
        }


        if (!this.errors.name && this.model.classId && this.model.teacherId) {
          await apiRequest(ApiConfig.baseUrlWith('subjects'), this.model, () => {
            (this.$parent as any)?.fetchData();
            this.$router.push('/subjects');
          }, ({ errors }) => errors.forEach(window.alert), { method: 'POST' });
        }
      }
    }
}

</script>
