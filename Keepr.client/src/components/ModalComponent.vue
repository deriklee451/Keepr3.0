<template>
  <div class="modal fade" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-lg">
      <div class="modal-content">


        <slot>

        </slot>

      </div>
    </div>
  </div>
</template>


<script>
export default {
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.modal {
  background: #abbaab;
  /* fallback for old browsers */
  background: -webkit-linear-gradient(to right, #ffffff, #abbaab);
  /* Chrome 10-25, Safari 5.1-6 */
  background: linear-gradient(to right, #ffffff, #abbaab);
  /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */



}
</style>