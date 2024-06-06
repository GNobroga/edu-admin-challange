<template>
  <div class="menu">
    <div class="bg-white p-5 max-w-[40vw] w-full rounded-md shadow-md py-8 animate-slide-in">
      <div class="flex justify-between gap-2 items-center">
        <h1 class="font-semibold text-2xl text-blue-900">
          <i class="bi bi-bar-chart-line-fill"></i>
          Médias</h1>
        <button class="fab-button bg-blue-800 text-white" @click="$router.push('/students')">
          <i class="bi bi-x text-2xl"></i>
        </button>
      </div>
      <table class="w-full text-center mt-8 border border-black border-opacity-40 shadow-lg">
        <thead class="bg-blue-800 text-white">
          <tr>
            <th><span class="block py-2">Disciplina</span></th>
            <th>Nota</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(average, index) of averages" :key="index">
            <td>
              <span class="block py-2">{{  average.name }}</span>
            </td>
            <td>
              {{  average.average }}
            </td>
          </tr>
          <tr v-if="!averages.length">
            <td colspan=2>
              <p class="text-base py-5">Não há notas lançadas</p>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>


<script lang="ts">
import { ApiConfig } from '~/environments/api-config';
import { apiRequest } from '~/utils/api-request';

  interface Average {
    name: string;
    average: number;
  }

  export default {
    data() {
      return {
        averages: [] as Average[],
      }
    },
    async created() {
      const id = this.$router.currentRoute.params.id;
      await apiRequest(ApiConfig.baseUrlWith(`grades/student/${id}/average`), undefined, data => this.averages = data);
    }
  }

</script>

