resource "azurerm_resource_group" "dreamroomsrg" {
  name     = "${local.dreamrooms_prefix}rg"
  location = local.location.north_europe
}