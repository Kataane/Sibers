<template>
  <i-form>
    <div class="form-group-wrapper">
      <i-form-group>
        <i-form-label>Implementer name</i-form-label>
        <i-input v-model="input" placeholder="Type company name" />
      </i-form-group>
      <i-form-group class="_margin-top:0">
        <i-form-label>Client name</i-form-label>
        <i-input v-model="input" placeholder="Type company name" />
      </i-form-group>
    </div>
  </i-form>
</template>
