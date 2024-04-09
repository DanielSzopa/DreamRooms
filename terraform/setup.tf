terraform {
  required_providers {
    azurerm = {
        source = "hashicorp/azurerm",
        version = "~> 3.98.0"
    }
  }

  required_version = "~> 1.7.5"

  backend "azurerm" {
    resource_group_name = "DreamRoomsState"
    storage_account_name = "dreamroomstatestorage"
    container_name = "state"
    key = "dreamrooms.state"
  }
}

provider "azurerm" {
  features {
    
  }
}

resource "azurerm_resource_group" "dreamRooms-rg" {
  name = "DreamRoomsRg"
  location = "North Europe"
}