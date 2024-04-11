variable "tenant_id" {
  type = string
  sensitive = true
}

locals {
  location = {
    north_europe = "North Europe"
  }
}

locals {
  dreamrooms_prefix = "dreamrooms"
}