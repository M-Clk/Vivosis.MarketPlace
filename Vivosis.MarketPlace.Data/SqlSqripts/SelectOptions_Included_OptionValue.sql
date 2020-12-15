SELECT 
o.option_id as 'option_id', 
o.type  as 'option_type', 
o.sort_order  as 'option_sort_order', 
od.name  as 'option_name', 
ovd.option_value_id as 'option_value_id',
ov.sort_order as 'option_value_sort_order',
ovd.name as 'option_value_name'
FROM `option` as o
Left join option_description as od on od.option_id = o.option_id
Left join option_value as ov on ov.option_id = o.option_id
left join option_value_description as ovd on ovd.option_id = o.option_id