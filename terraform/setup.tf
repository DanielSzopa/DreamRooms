terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm",
      version = "~> 3.98.0"
    }
  }

  required_version = "~> 1.7.5"

  backend "azurerm" {
    resource_group_name  = "dreamroomsstaterg"
    storage_account_name = "dreamroomsstatestorage"
    container_name       = "state"
    key                  = "dreamrooms.state"
  }
}

provider "azurerm" {
  features {

  }
}

resource "azurerm_resource_group" "dreamrooms_state_rg" {
  name     = "${local.dreamrooms_prefix}staterg"
  location = local.location.north_europe
}

resource "azurerm_storage_account" "state_sa" {
  name                     = "${local.dreamrooms_prefix}statestorage"
  resource_group_name      = azurerm_resource_group.dreamrooms_state_rg.name
  location                 = azurerm_resource_group.dreamrooms_state_rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_container" "state_container" {
  name                  = "state"
  storage_account_name  = azurerm_storage_account.state_sa.name
  container_access_type = "private"
}