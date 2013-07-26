IFDatahouse::Application.routes.draw do
  root :to=>"sessions#new"

  controller :sessions do
    get 'login' => :new
    post 'login' => :create
    get 'logout' => :destroy
    get 'index'=>:index
  end
  
  [:part_infos,:parts,:staffs,:entities].each do |model|
    resources model do
      collection do
        post :updata
        get :import
        get :download
        get :search
      end
    end
  end
 
  resources :mappers do
    collection do
      get :cancel
      match :map
      post :domap
      get 'c'=>:create
    end
    member do
      get :select
      get :item
    end
  end

  resources :mapper_items

  namespace :api,defaults:{format:'json'} do
    controller :file do
      match 'file/part'=>:part
      match 'file/staff'=>:staff
      match 'file/entity'=>:entity
    end
  end
end
