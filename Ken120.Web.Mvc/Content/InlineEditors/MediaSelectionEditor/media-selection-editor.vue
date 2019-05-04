<template>
  <div class="root">
    <div class="row">
      <div class="col-lg-3">
        <div class="row">
          <div class="col-lg-12">
            <div class="selected-media mb-4">
              <div>
                <h4>Selected</h4>
                <img :src="selectedMediaFullUrl" v-if="selectedMediaUrl">
                <span v-else>No media selected</span>
              </div>
              <button
                class="btn btn-outline-secondary btn-sm"
                @click="isLibraryVisible = !isLibraryVisible"
              >
                <span v-if="!isLibraryVisible">Show Library</span>
                <span v-else>Hide Library</span>
              </button>
            </div>
          </div>
        </div>
        <div class="row" v-if="isLibraryVisible">
          <div class="col-lg-12">
            <form @submit.prevent="onSearch">
              <div class="form-group">
                <label class="control-label">Page Size</label>
                <input class="form-control" type="number" v-model="params.pageSize">
              </div>

              <div class="form-group">
                <label class="control-label">Page Number</label>
                <input class="form-control" type="number" v-model="params.pageNumber">
              </div>

              <div class="form-group">
                <label class="control-label">Title</label>
                <input class="form-control" type="text" v-model="params.fileTitle">
              </div>

              <div class="form-group">
                <label class="control-label">Filename</label>
                <input class="form-control" type="text" v-model="params.fileName">
              </div>

              <div class="form-group">
                <button type="submit" class="btn btn-primary">Search</button>
              </div>
            </form>
          </div>
        </div>
      </div>
      <div class="col-lg-9" v-if="isLibraryVisible">
        <h4>Available</h4>
        <div class="card-desk row media-gallery flex-wrap">
          <template v-for="(media) in medias">
            <div class="col-3 mb-4" v-bind:key="media.fileGUID">
              <div class="card border border-secondary rounded h-100">
                <div class="d-flex justify-content-center card-img-top">
                  <img
                    class
                    :id="media.fileID"
                    :src="getMediaUrl(media)"
                    :alt="media.fileTitle"
                    @click="onDisplay(media)"
                  >
                </div>
                <div class="content">
                  <div class="card-body">
                    <h5 class="card-title text-secondary">{{ media.fileTitle }}</h5>
                    <p class="card-text text-dark">{{ media.fileName }}</p>
                    <button class="btn btn-outline-primary btn-sm" @click="onSelect(media)">Select</button>
                  </div>
                  <div class="card-footer">
                    <small
                      class="text-muted"
                    >{{ media.fileImageWidth }} x {{ media.fileImageHeight }} px</small>
                    <small class="text-muted">{{ media.fileSize / 1000 }} kb</small>
                  </div>
                </div>
              </div>
            </div>
          </template>
        </div>
      </div>
    </div>
    <div
      v-if="modalMedia"
      class="modal"
      :class="{ 'd-block': modalMedia }"
      tabindex="-1"
      role="dialog"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">{{ modalMedia.fileTitle }}</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true" @click="onDisplay(undefined)">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <img
              :id="modalMedia.fileID"
              :src="getMediaUrl(modalMedia, 400)"
              :alt="modalMedia.fileTitle"
              @click="onDisplay(undefined)"
            >
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" @click="onSelect(modalMedia)">Select</button>
            <button
              type="button"
              class="btn btn-secondary"
              data-dismiss="modal"
              @click="onDisplay(undefined)"
            >Close</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    hostUrl: String,
    selectedMediaUrl: String
  },

  data: function() {
    return {
      params: {
        fileTitle: "",
        fileName: "",
        pageSize: "4",
        pageNumber: "1"
      },
      maxSideSize: 150,
      medias: [],
      modalMedia: undefined,
      isLibraryVisible: false
    };
  },
  methods: {
    onSelect(media) {
      this.modalMedia = undefined;

      this.$emit("dispatch", media);
    },
    onDisplay(media) {
      this.modalMedia = media;
    },
    onSearch() {
      fetch(
        `${
          this.hostUrl
        }/CMSPages/MediaLibraryApi.ashx?${this.getQueryStringParms(
          this.prepareParams(this.params)
        )}`,
        {
          credentials: "include"
        }
      )
        .then(resp => resp.json())
        .then(medias => (this.medias = medias))
        .catch(error => console.log(error));
    },
    prepareParams(params) {
      return {
        ...params,
        pageNumber: params.pageNumber - 1
      };
    },
    getQueryStringParms: function(params) {
      return Object.keys(params).reduce(
        (accumulator, currentKey) =>
          `${accumulator}${accumulator ? "&" : ""}${currentKey}=${
            params[currentKey]
          }`,
        ""
      );
    },
    getMediaUrl(media, maxSideSize) {
      return `${this.hostUrl}${media.permanentUrl}?maxsidesize=${maxSideSize ||
        this.maxSideSize}`;
    }
  },
  computed: {
    selectedMediaFullUrl: function() {
      return `${this.hostUrl}${this.selectedMediaUrl}?maxsidesize=${
        this.maxSideSize
      }`;
    }
  },
  created: function() {
    this.onSearch();
  }
};
</script>

<style lang="scss" scoped>
.root {
  padding: 1rem;
}

.selected-media {
  display: flex;
  align-items: flex-end;
  > * {
    margin-right: 1rem;
  }
}

.card {
  justify-content: space-between;
}

.card-img-top {
  padding-top: 0.5rem;
  width: auto;
}

.card-footer {
  display: flex;
  justify-content: space-between;
}

.modal {
  z-index: 10011; /* one above Kentico's widget header z-index */
}
</style>
