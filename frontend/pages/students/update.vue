<template>
      <menu class="bg-black bg-opacity-30 absolute top-0 left-0 w-full h-full flex items-center justify-center">
      <form class="bg-white px-7 py-10 max-w-[35vw] w-full shadow-lg rounded-md flex flex-col gap-3 animate-slide-in">
        <div class="flex justify-between">
          <h1 class="font-semibold text-lg mb-2">{{  data.id ? 'Atualização' : 'Novo' }}</h1>
          <button type="button" @click="$router.push('./')" class="w-8 h-8 relative bottom-2 rounded-full flex items-center justify-center hover:bg-gray-200">
            <i class="bi bi-x text-2xl"></i>
          </button>
        </div>
       <div v-if="!data.id" class="flex gap-3">
          <div class="flex flex-col gap-2 flex-1">
            <label class="opacity-80">Nome</label>
            <input v-model="data.name" type="text" name="name" class="border rounded-md border-black border-opacity-30 py-3 px-4 outline-none" placeholder="Nome">
          </div>
          <div class="flex flex-col gap-2 flex-1">
            <label class="opacity-80">Sobrenome</label>
            <input v-model="data.lastName" type="text" name="lastName" class="border rounded-md border-black border-opacity-30 py-3 px-4 outline-none" placeholder="Sobrenome">
          </div>
       </div>
       <div v-if="data.id" class="flex gap-3">
        <div class="flex flex-col gap-2 flex-1">
              <label class="opacity-80">Matrícula</label>
              <input v-bind:value="'AL' + data.id" disabled type="text" name="name" class="border bg-gray-200 rounded-md border-black border-opacity-30 py-3 px-4 outline-none" placeholder="Nome">
            </div>
          <div class="flex flex-col gap-2 flex-1">
              <label class="opacity-80">Nome Completo</label>
              <input v-model="data.name" type="text" name="name" class="border rounded-md border-black border-opacity-30 py-3 px-4 outline-none" placeholder="Nome">
            </div>
        </div>
        <div class="flex flex-col gap-2">
          <label class="opacity-80">E-mail</label>
          <input v-model="data.email" type="email" name="email" class="border rounded-md border-black border-opacity-30 py-3 px-4 outline-none" placeholder="Email">
        </div>
        <span class="flex justify-between mt-5 gap-3">
          <button type="button" @click="save()" class=" bg-blue-500 hover:opacity-90 flex-1 text-white py-3 rounded-md uppercase shadow-lg">Salvar</button>
        </span>
      </form>
    </menu>
</template>

<script lang="ts">
import { ApiConfig } from '~/environments/api-config';


  export default {
    data() {
      return {
        data: {
          id: '',
          name: '',
          lastName: '',
          email: '',
          type: 'STUDENT',
        },
      };
    },
    created() {
      this.ifEdition();
    },
    methods: {
      async save() {
        if (!this.data.email?.trim() || !this.data.name?.trim() || (!this.data.id && !this.data.lastName?.trim())) {
           window.alert('Preecha todos os campos');
          return;
        }

        if (!/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/.test(this.data.email)) {
          window.alert('Insira um e-mail válido');
          return;
        }

        if (this.data.lastName) {
          this.data.name = `${this.data.name} ${this.data.lastName}`;
        }

        if (this.data.id) {
          const req = await fetch(ApiConfig.baseUrlWith('usuarios/'+this.data.id), {
            method: 'put',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(this.data)
          });

          if (!req.ok) {
            const error = (await req.json()) as ResponseError;
            window.alert(error.message);
            return;
          }

          await this.$parent.fetchData();
          this.$router.push('./');
          return;
        }

       const req = await fetch(ApiConfig.baseUrlWith('usuarios'), {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(this.data)
          });

          if (!req.ok) {
            const error = (await req.json()) as ResponseError;
            window.alert(error.message);
            return;
          }

          await this.$parent.fetchData();
          this.$router.push('./');
      },
      ifEdition() {
        if (!this.$router.currentRoute.query?.studentId) return;
        this.data.id = parseInt(this.$router.currentRoute.query?.studentId);
        this.findById(this.data.id);
      },
      async findById(id: number) {
        const req = await fetch(ApiConfig.baseUrlWith('usuarios/'+id));
        this.data = (await req.json()).data;
      }
    },
  }

</script>
