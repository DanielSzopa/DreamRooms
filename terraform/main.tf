resource "azurerm_resource_group" "dreamroomsrg" {
  name     = "${local.dreamrooms_prefix}rg"
  location = local.location.north_europe
}

resource "azurerm_key_vault" "dreamrooms_key_vault" {
  name = "${local.dreamrooms_prefix}kv"
  location = azurerm_resource_group.dreamroomsrg.location
  resource_group_name = azurerm_resource_group.dreamrooms_state_rg.name
  sku_name = "standard"
  tenant_id = var.tenant_id
  enable_rbac_authorization = true
}