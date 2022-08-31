<template>
  <i-form>
    <i-form-group>
      <i-form-label>Project Name</i-form-label>
      <i-input v-model="input" placeholder="Type project name" />
    </i-form-group>
    <div class="form-group-wrapper _margin-top:2/3">
      <i-form-group>
        <i-form-label>Project start date</i-form-label>
        <Datepicker v-model="startDate"></Datepicker>
      </i-form-group>
      <i-form-group class="_margin-top:0">
        <i-form-label>Project end date</i-form-label>
        <Datepicker v-model="endDate"></Datepicker>
      </i-form-group>
    </div>
  </i-form>
</template>

<script setup>
import Datepicker from "@vuepic/vue-datepicker";

const startDate = ref();
const endDate = ref();
</script>
