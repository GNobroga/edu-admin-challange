<template>
  <menu class="menu">
    <form
      @submit.prevent="validateForm"
      class="bg-white px-7 py-10 max-w-[35vw] w-full shadow-lg rounded-md flex flex-col gap-3"
    >
      <div class="flex justify-between">
        <h1 class="font-bold text-2xl mb-2">Novo</h1>
        <button
          type="button"
          @click="$router.push('/classes')"
          class="fab-button bg-blue-800 relative bottom-2 text-white"
        >
          <i class="bi bi-x text-3xl"></i>
        </button>
      </div>
      <div class="flex gap-3">
        <div class="flex flex-col gap-2 flex-1">
          <label class="opacity-80">Código</label>
          <input
            type="text"
            v-model="model.id"
            disabled
            class="border bg-gray-100 rounded-md border-black border-opacity-30 text-lg py-4 px-4 outline-none"
            placeholder="Nome"
          />
        </div>
      </div>

      <div class="flex gap-3">
        <div class="flex flex-col gap-2 flex-1">
          <label class="opacity-80">Nome</label>
          <input
            type="text"
            v-model="model.name"
            name="name"
            class="border rounded-md border-black border-opacity-30 text-lg py-4 px-4 outline-none"
            placeholder="Nome"
          />
          <span v-if="errors.name" class="text-xs text-red-600 ms-2">{{
            errors.name
          }}</span>
        </div>
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
import { apiRequest } from "~/utils/api-request";

export default {
  data: () => ({
    model: {
      id: "",
      name: "",
    },
    errors: {
      name: "",
    },
  }),
  async created() {
    await apiRequest(ApiConfig.baseUrlWith(`classes/${this.$router.currentRoute.params.id}`), undefined, data => {
      this.model = data;
    });
  },
  methods: {
    async validateForm() {
      this.errors = {};

      if (this.model.name.trim() === "") {
        this.errors.name = "O campo é obrigatório";
      }

      if (!this.errors.name) {
        await apiRequest(
          ApiConfig.baseUrlWith(`classes/${this.model.id}`),
          this.model,
          () => {
            (this.$parent as any)?.fetchData();
            this.$router.push("/classes");
          },
          ({ errors }) => errors.forEach(window.alert),
          { method: "PUT" }
        );
      }
    },
  },
};
</script>
