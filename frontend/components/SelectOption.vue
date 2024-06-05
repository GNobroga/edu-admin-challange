<template>
  <div class="w-full h-full relative border  rounded-md border-black border-opacity-30 text-lg  px-2 outline-none">
    <select ref="select" class="w-full outline-none border-none h-full py-4" @input="selectItem">
      <option value="" disabled selected>Selecione</option>
      <option v-for="option of options" :key="option.id" v-bind:value="option.id">{{ formatOption(option) }}</option>
    </select>
  </div>
</template>

<script lang="ts">
  export default {
    props: ['excludeProperties', 'options', 'item'],
    mounted() {
      setTimeout(() => {
        (this.$refs.select as any).value = this.item.id;
        console.log(this.item.id)
      }, 300);


    },
    methods: {
      selectItem(event: any) {
        this.$emit('id', { id: event.target.value });
      },
      formatOption(option: any) {
        if (typeof option != 'object') return '';
        const values = [] as any[];
        for (let key in option) {
          if (!this.excludeProperties?.includes(key)) {
            values.push(option[key]);
          }
        }
        return values.join(' - ');
      }
    }
  };
</script>

<style scoped>

</style>
