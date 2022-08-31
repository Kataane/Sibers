<template>
  <i-container>
    <i-row>
      <i-column push="3" xs="5">
        <i-card class="_margin-top:5">
          <component :is="active" />
        </i-card>

        <i-button-group class="_margin-top:1" block>
          <i-button v-if="stage > 0" @click="prev" outline color="light"
            >Back</i-button
          >
          <i-button
            v-if="stage < stages - 1"
            @click="next"
            outline
            color="light"
            >Next</i-button
          >
        </i-button-group>
      </i-column>
    </i-row>
  </i-container>
</template>

<script setup lang="ts">
const stage = ref(0);
const stages = 5;

const arr = ["project", "companies", "supervisor", "employees", "documents"];

const active = ref("project");

const router = useRouter();

function prev() {
  if (stage.value == stages) return;
  stage.value--;
  active.value = arr[stage.value];
}

function next() {
  if (stage.value == stages) return;
  stage.value++;
  active.value = arr[stage.value];
}
</script>

<style lang="scss">
@import "@inkline/inkline/css/variables";
@import "@inkline/inkline/css/mixins";

.dp__theme_light {
  --dp-background-color: #ffffff;
  --dp-text-color: #212121;
  --dp-hover-color: #f3f3f3;
  --dp-hover-text-color: #212121;
  --dp-hover-icon-color: #959595;
  --dp-primary-color: #1976d2;
  --dp-primary-text-color: #f8f5f5;
  --dp-secondary-color: #c0c4cc;
  --dp-border-color: #ddd;
  --dp-menu-border-color: #ddd;
  --dp-border-color-hover: #aaaeb7;
  --dp-disabled-color: #f6f6f6;
  --dp-scroll-bar-background: #f3f3f3;
  --dp-scroll-bar-color: #959595;
  --dp-success-color: #76d275;
  --dp-success-color-disabled: #a3d9b1;
  --dp-icon-color: #959595;
  --dp-danger-color: #ff6f60;
  --dp-highlight-color: rgba(25, 118, 210, 0.1);
}

$dp__font_family: var(--font-family-primary--base) !default;
$dp__border_radius: 0.1rem !default;
$dp__button_height: 0px !default;
$dp__month_year_row_height: 35px !default;
$dp__month_year_row_button_size: 16px !default;
$dp__cell_size: 30px !default;

@import "@vuepic/vue-datepicker/src/VueDatePicker/style/main.scss";

:root {
  --color--primary--h: 195deg;
  --color--primary--s: 77%;
  --color--primary--l: 39%;

  --border-top-left-radius: 0.1rem;
  --border-top-right-radius: 0.1rem;
  --border-bottom-right-radius: 0.1rem;
  --border-bottom-left-radius: 0.1rem;

  --box-shadow-blur-radius: 0;
}

.form-group-wrapper {
  display: flex;

  .form-group {
    margin-top: 0;
    width: 100%;
  }

  .form-group + .form-group {
    margin-left: var(--margin-left);
  }
}
</style>
