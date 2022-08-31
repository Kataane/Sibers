<template>
  <i-form-group>
    <i-form-label>Select supervisor for project</i-form-label>
    <i-select
      placeholder="Supervisor"
      v-model="employee"
      :options="options"
      :clearable="true"
      :loading="loading"
      @search="onSearch"
      autocomplete
    />
  </i-form-group>
</template>

<script setup lang="ts">
const employee = ref<Employee>();

export type Employee = {
  id: number;
  label: string;
};

let query = "~";
const loading = ref(false);
const options = ref([]);

async function onSearch(_query: string) {
  if (!_query) return (query = "");
  if (_query.length < 2 || _query === query) return;

  loading.value = true;
  query = _query;
  options.value = await fetchAreas();
  loading.value = false;
}

async function fetchAreas() {
  try {
    const { data } = await $fetch<IEmployees>(
      "https://localhost:7089/Employees",
      {
        params: { fullName: query },
      }
    );
    return data.map((item) => ({ id: item.id, label: item.fullName }));
  } catch (error) {
    console.log(error);
  }
}

interface IEmployee {
  id: string;
  fullName: string;
}

interface IEmployees {
  data: IEmployee[];
}
</script>
