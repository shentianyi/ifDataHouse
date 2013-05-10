#encoding: utf-8

config=YAML.load(File.open("#{Rails.root}/config/config.yaml"))
# load format
format_config=config['format']
$CSVSP=format_config[:csv_splitor] # csv splitor
$UPMARKER=format_config[:update_marker]
#load path
path_config=config['path']
$UPDATAPATH=path_config[:updata_file_path] # demand csv file save path

