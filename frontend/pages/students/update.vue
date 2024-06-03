<template>
      <menu class="bg-black bg-opacity-30 absolute top-0 left-0 w-full h-full flex items-center justify-center">
      <form class="bg-white px-7 py-10 max-w-[30vw] w-full shadow-lg rounded-md flex flex-col gap-3 animate-slide-in">
        <h1 class="font-semibold text-lg mb-2">Novo</h1>
        <div class="flex flex-col gap-2">
          <label class="opacity-80">Nome</label>
          <input v-model="data.name" type="text" name="name" class="border border-black border-opacity-30 py-3 px-4 outline-none" placeholder="Nome">
        </div>
        <div class="flex flex-col gap-2">
          <label class="opacity-80">E-mail</label>
          <input v-model="data.email" type="email" name="email" class="border border-black border-opacity-30 py-3 px-4 outline-none" placeholder="Email">
        </div>
        <span class="flex justify-between mt-3 gap-3">
          <button @click="$router.push('./')" type="button"class=" bg-gray-200 hover:opacity-90  flex-1 text-black py-3 rounded-md shadow-lg border border-black border-opacity-10">Voltar</button>
          <button type="button" @click="save()" class=" bg-blue-500 hover:opacity-90 flex-1 text-white py-3 rounded-md shadow-lg">Salvar</button>
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
        if (!this.data.email?.trim() || !this.data.name?.trim()) {
          window.alert('Preecha todos os campos');
          return;
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
