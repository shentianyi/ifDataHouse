# This file should contain all the record creation needed to seed the database with its default values.
# The data can then be loaded with the rake db:seed (or created alongside the db with db:setup).
#
# Examples:
#
#   cities = City.create([{ name: 'Chicago' }, { name: 'Copenhagen' }])
#   Mayor.create(name: 'Emanuel', city: cities.first)
if !staff=Staff.find_by(:staffNr=>'admin')
  Staff.create(:staffNr=>'admin',:name=>'admin',:password=>'admin@')
else
  puts "staffNr: admin already in db:#{staff.id}.".red
  # staff.parts<<Part.new(:partNr=>'333')
  # staff.parts<<Part.new(:partNr=>'33344')
end
 