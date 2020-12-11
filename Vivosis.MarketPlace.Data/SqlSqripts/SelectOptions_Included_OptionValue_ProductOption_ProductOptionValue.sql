SELECT 
o.option_id as 'option_id', 
o.type  as 'option_type', 
od.name  as 'option_name', 
o.sort_order  as 'option_sort_order', 
po.product_option_id as 'product_option_id',
po.required as 'product_option_required',
po.value as 'product_option_value',
po.product_id as 'product_option_product_id',
ov.option_value_id  as 'option_value_id', 
ovd.name as 'option_value_name',
ov.sort_order as 'option_value_sort_order',
pov.product_option_value_id as 'product_option_value_id',
pov.quantity as 'product_option_value_quantity',
CONCAT(pov.price_prefix, pov.price) as 'product_option_value_price',
CONCAT(pov.points_prefix, pov.points) as 'product_option_value_points',
CONCAT(pov.weight_prefix, pov.weight) as 'product_option_value_weight'
FROM `option` as o
Left join option_description as od on od.option_id = o.option_id
Left join option_value as ov on ov.option_id = o.option_id
left join option_value_description as ovd on ovd.option_id = o.option_id
left join product_option as po on po.option_id = o.option_id
left join product_option_value as pov on pov.product_option_id = po.product_option_id
